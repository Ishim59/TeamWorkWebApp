using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TeamWorkWebApp.Interfaces;
using TeamWorkWebApp.ViewModels;

namespace TeamWorkWebApp.Controllers
{
    public class TasksController : Controller
    {
        private readonly IAppRepository _appRepository;
        private TasksViewModel _taskViewModel;
        public TasksController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }
        public async Task<IActionResult> Index(TasksViewModel tasksViewModel)
        {
            _taskViewModel = tasksViewModel;
            _taskViewModel.Tasks = await _appRepository.GetTasksOfExecutorAsync(
                await _appRepository.GetUserByIdAsync(_taskViewModel.Id).ConfigureAwait(false),
                await _appRepository.GetGroupByIdAsync(_taskViewModel.GroupId).ConfigureAwait(false)).ConfigureAwait(false);
            _taskViewModel.SelectedTask = _taskViewModel.Tasks.FirstOrDefault(t => t.Id == _taskViewModel.SelectedTaskId);
            return View(_taskViewModel);
        }
        [HttpPost]
        public IActionResult ShowTask(TasksViewModel tasksViewModel)
        {
            _taskViewModel = tasksViewModel;
            return RedirectToAction("Index", _taskViewModel);
        }
    }
}
