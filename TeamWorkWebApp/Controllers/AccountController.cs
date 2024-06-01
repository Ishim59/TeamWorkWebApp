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
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Unable to access your account. Check the correctness of the entered data";
                return RedirectToAction("SignIn", "Account");
            }
            if(!_appRepository.UserExists(accountViewModel.Email, accountViewModel.Password).Result)

            return RedirectToAction("Privacy", "Home");
        }
        [HttpPost]
        public IActionResult Registration(AccountViewModel accountViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Unable to access your account. Check the correctness of the entered data";
                return RedirectToAction("SignUp", "Account");
            }
            if(accountViewModel.Password != accountViewModel.PasswordConfirmation)
                return RedirectToAction("SignUp", "Account");

            return _appRepository.AddUser(accountViewModel.Email, accountViewModel.Password, accountViewModel.Name)
                .Result ? RedirectToAction("Privacy", "Home") : RedirectToAction("SignUp", "Account");
        }
    }
}
