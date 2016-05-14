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
            ViewData["ContentHeader"] = "Umsækjandi";
            ViewData["selectedNavPillId"] = "navPillApplicant";
            return View();
        }

        [Route("Menntun")]
        public IActionResult Education()
        {
            ViewData["ContentHeader"] = "Menntun";
            ViewData["selectedNavPillId"] = "navPillEducation";
            return View();
        }

        [Route("Starfsferill")]
        public IActionResult Employment()
        {
            ViewData["ContentHeader"] = "Starfsferill";
            ViewData["selectedNavPillId"] = "navPillEmployment";
            return View();
        }

        [Route("Fjarmal")]
        public IActionResult Finances()
        {
            ViewData["ContentHeader"] = "Fjármál";
            ViewData["selectedNavPillId"] = "navPillFinances";
            return View();
        }

        [Route("Lan")]
        public IActionResult Loan()
        {
            ViewData["ContentHeader"] = "Lán";
            ViewData["selectedNavPillId"] = "navPillLoan";
            return View();
        }

        [Route("Samatekt")]
        public IActionResult Summary()
        {
            ViewData["ContentHeader"] = "Samatekt";
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
