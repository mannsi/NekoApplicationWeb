using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ViewModels;
using NekoApplicationWeb.ViewModels.Account;

namespace NekoApplicationWeb.Controllers
{
    [Route("")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(ILogger<AccountController> logger, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("")]
        public IActionResult StartPage()
        {
            var vm = new StartPageViewModel();
            return View();
        }

        [Route("GleymtLykilord")]
        public IActionResult ForgotPassword()
        {
            return View("StartPage");
        }

        [HttpPost]
        [Route("Innskra")]
        public async Task<IActionResult> Login(StartPageViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            var user = await _userManager.FindByNameAsync(vm.Ssn.Replace("-", ""));
            if (user == null)
            {
                return View("Error");
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);
            if (signInResult.Succeeded)
            {
                return RedirectToAction("Index", "Page");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        [Route("Utskra")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("StartPage");
        }
    }
}
