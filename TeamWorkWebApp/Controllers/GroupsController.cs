using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TeamWorkWebApp.Interfaces;
using TeamWorkWebApp.Models;
using TeamWorkWebApp.ViewModels;

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
                _groupsViewModel.Groups = _cache.Get<List<Group>?>(_groupsCacheKey);
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
            
            
            return View(_groupsViewModel);
        }
    }
}
