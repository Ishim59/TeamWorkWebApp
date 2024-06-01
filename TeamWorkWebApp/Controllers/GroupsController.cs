using Microsoft.AspNetCore.Mvc;
using TeamWorkWebApp.Interfaces;

namespace TeamWorkWebApp.Controllers
{
    public class GroupsController : Controller
    {
        private readonly IAppRepository _appRepository;

        public GroupsController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
