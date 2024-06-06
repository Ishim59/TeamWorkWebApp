using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TeamWorkWebApp.Interfaces;
using TeamWorkWebApp.Models;
using TeamWorkWebApp.ViewModels;
using Task = System.Threading.Tasks.Task;

namespace TeamWorkWebApp.Controllers
{
    public class GroupsController : Controller
    {
        private readonly IAppRepository _appRepository;
        private readonly IMemoryCache _cache;
        private readonly string _groupsCacheKey = "groupsCacheKey";
        private GroupsViewModel _groupsViewModel;
        
        public GroupsController(IAppRepository appRepository, IMemoryCache cache)
        {
            _appRepository = appRepository;
            _cache = cache;
        }
        public async Task<IActionResult> IndexAsync(GroupsViewModel groupsViewModel)
        {
            _groupsViewModel = groupsViewModel;
            if (_cache.TryGetValue(_groupsCacheKey, out IEnumerable<Group>? groups))
            {
                _groupsViewModel.Groups = (List<Group>?)groups;
            }
            else
            {
                _groupsViewModel.Groups = (List<Group>?)await _appRepository.GetGroupsByUserAsync(_groupsViewModel.Id).ConfigureAwait(false);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                    .SetPriority(CacheItemPriority.Normal);

                _cache.Set(_groupsCacheKey, _groupsViewModel.Groups, cacheEntryOptions);
            }

            if (groupsViewModel.SelectedGroupId != 0)
            {
                _groupsViewModel.SelectedGroup =
                    await _appRepository.GetGroupByIdAsync(_groupsViewModel.SelectedGroupId).ConfigureAwait(false);
                _groupsViewModel.SelectedGroupMembers =
                    (List<User>)await _appRepository.GetGroupMembersAsync(_groupsViewModel.SelectedGroupId).ConfigureAwait(false);
            }
            
            return View(_groupsViewModel);
        }

        public Task<IActionResult> ShowGroup(GroupsViewModel groupsViewModel)
        {
            _groupsViewModel = groupsViewModel;
            return Task.FromResult<IActionResult>(RedirectToAction("Index", _groupsViewModel));
        }
    }
}
