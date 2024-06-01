using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TeamWorkWebApp.Interfaces;
using TeamWorkWebApp.Models;
using TeamWorkWebApp.Repositories;
using TeamWorkWebApp.ViewModels;

namespace TeamWorkWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAppRepository _appRepository;
        private readonly IMemoryCache _cache;
        public AccountController(IAppRepository appRepository, IMemoryCache cache)
        {
            _appRepository = appRepository;
            _cache = cache;
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
                ViewBag.Message = "Failed to SignIn";
                return RedirectToAction("SignIn", "Account");
            }
            if(!await _appRepository.UserExistsAsync(signInViewModel.Email, signInViewModel.Password).ConfigureAwait(false))
                return RedirectToAction("SignIn", "Account");
            return RedirectToAction("Index", "Groups", new GroupsViewModel(){Id = await _appRepository.GetUserIdAsync(signInViewModel.Email).ConfigureAwait(false)});
        }
        [HttpPost]
        public async Task<IActionResult> RegistrationAsync(SignUpViewModel signUnViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Failed to SignUp";
                return RedirectToAction("SignUp", "Account");
            }
            if(signUnViewModel.Password != signUnViewModel.PasswordConfirmation)
                return RedirectToAction("SignUp", "Account");
            return await _appRepository.AddUserAsync(signUnViewModel.Email, signUnViewModel.Password, signUnViewModel.Name).ConfigureAwait(false) ?
                RedirectToAction("Index", "Groups", new GroupsViewModel() { Id = await _appRepository.GetUserIdAsync(signUnViewModel.Email).ConfigureAwait(false) }) : RedirectToAction("SignUp", "Account");
        }
    }
}
