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
        public async Task<IActionResult> LoginAsync(SignInViewModel signInViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Unable to access your account. Check the correctness of the entered data";
                return RedirectToAction("SignIn", "Account");
            }
            if(!await _appRepository.UserExistsAsync(signInViewModel.Email, signInViewModel.Password).ConfigureAwait(false))
                return RedirectToAction("SignIn", "Account");
            return RedirectToAction("Privacy", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> RegistrationAsync(SignUpViewModel signUnViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Unable to access your account. Check the correctness of the entered data";
                return RedirectToAction("SignUp", "Account");
            }
            if(signUnViewModel.Password != signUnViewModel.PasswordConfirmation)
                return RedirectToAction("SignUp", "Account");
            return await _appRepository.AddUserAsync(signUnViewModel.Email, signUnViewModel.Password, signUnViewModel.Name).ConfigureAwait(false) ?
                RedirectToAction("Privacy", "Home") : RedirectToAction("SignUp", "Account");
        }
    }
}
