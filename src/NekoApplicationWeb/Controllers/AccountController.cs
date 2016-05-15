using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using NekoApplicationWeb.ViewModels;

namespace NekoApplicationWeb.Controllers
{
    //[Route("")]
    public class AccountController : Controller
    {
        //[Route("")]
        public IActionResult Login()
        {
            ViewData["ContentHeader"] = "Umsókn um Neko fasteignalán";
            return View();
        }
    }
}
