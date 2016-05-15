using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using NekoApplicationWeb.ViewModels;

namespace NekoApplicationWeb.Controllers
{
    [Route("")]
    public class AccountController : Controller
    {
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
        public IActionResult Login()
        {
            return RedirectToAction("Index", "Application");
        }
    }
}
