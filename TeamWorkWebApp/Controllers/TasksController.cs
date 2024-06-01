using Microsoft.AspNetCore.Mvc;

namespace TeamWorkWebApp.Controllers
{
    public class TasksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
