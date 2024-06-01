using Microsoft.AspNetCore.Mvc;
using TeamWorkWebApp.Interfaces;
using TeamWorkWebApp.Models;
using TeamWorkWebApp.Repositories;
using TeamWorkWebApp.ViewModels;

namespace TeamWorkWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAppRepository _appRepository;
        public AccountController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }
        public IActionResult SignIn()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(AccountViewModel accountViewModel)
        {
            
            return RedirectToAction("Privacy", "Home");
        }
        [HttpPost]
        public IActionResult Registration()
        {
            return RedirectToAction("Privacy", "Home");
        }
    }
}
