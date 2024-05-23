using Microsoft.AspNetCore.Mvc;

namespace TeamWorkWebApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult Login()
        {
            // TODO Check Email and Password
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Registration()
        {
            return RedirectToAction("Privacy", "Home");
        }
    }
}
