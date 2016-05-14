using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace NekoApplicationWeb.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            ViewData["ContentHeader"] = "Fyrsta sem notandi sér";
            return View();
        }

        [Route("Umsaekjandi")]
        public IActionResult Personal()
        {
            ViewData["ContentHeader"] = "Umsókn um Neko lán";
            ViewData["selectedNavPillId"] = "navPillApplicant";
            return View();
        }

        [Route("Menntun")]
        public IActionResult Education()
        {
            ViewData["ContentHeader"] = "Umsókn um Neko lán";
            ViewData["selectedNavPillId"] = "navPillEducation";
            return View();
        }

        [Route("Starfsferill")]
        public IActionResult Employment()
        {
            ViewData["ContentHeader"] = "Umsókn um Neko lán";
            ViewData["selectedNavPillId"] = "navPillEmployment";
            return View();
        }

        [Route("Fjarmal")]
        public IActionResult Finances()
        {
            ViewData["ContentHeader"] = "Umsókn um Neko lán";
            ViewData["selectedNavPillId"] = "navPillFinances";
            return View();
        }

        [Route("Lan")]
        public IActionResult Loan()
        {
            ViewData["ContentHeader"] = "Umsókn um Neko lán";
            ViewData["selectedNavPillId"] = "navPillLoan";
            return View();
        }

        [Route("Samatekt")]
        public IActionResult Summary()
        {
            ViewData["ContentHeader"] = "Umsókn um Neko lán";
            ViewData["selectedNavPillId"] = "navPillSummary";
            return View();
        }

        [Route("Error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
