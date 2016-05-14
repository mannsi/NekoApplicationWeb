﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using NekoApplicationWeb.ViewModels;

namespace NekoApplicationWeb.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            ViewData["ContentHeader"] = "Umsókn um Neko fasteignalán";
            return View();
        }

        [Route("Umsaekjandi")]
        [HttpGet]
        public IActionResult Personal()
        {
            ViewData["ContentHeader"] = "Umsækjendur";
            ViewData["selectedNavPillId"] = "navPillApplicant";

            var vm = new PersonalViewModel
            {
                Name = "Jón Jónsson",
                Email = "jon@corporate.com",
                Ssn = "1203952159"
            };
            return View(vm);
        }

        [Route("PersonalForward")]
        [HttpPost]
        public IActionResult PersonalForward(PersonalViewModel vm)
        {
            return RedirectToAction("Education");
        }

        [Route("PersonalBackwards")]
        [HttpPost]
        public IActionResult PersonalBackwards(PersonalViewModel vm)
        {
            return RedirectToAction("Index");
        }

        [Route("Menntun")]
        public IActionResult Education()
        {
            ViewData["ContentHeader"] = "Menntun";
            ViewData["selectedNavPillId"] = "navPillEducation";

            var vm = new EducationViewModel()
            {
                Degrees =  new List<EducationDegree>()
                {
                    new EducationDegree() { School = "Háskólinn í Reykjavík", Degree = "MSc í Tölvunarfræði", DateFinished = new DateTime(2018, 6, 1)}
                },
                DegreesSpouse = new List<EducationDegree>()
                {
                    new EducationDegree()
                }
            };
            return View(vm);
        }

        [Route("EducationForward")]
        [HttpPost]
        public IActionResult EducationForward(PersonalViewModel vm)
        {
            return RedirectToAction("Employment");
        }

        [Route("EducationBackwards")]
        [HttpPost]
        public IActionResult EducationBackwards(PersonalViewModel vm)
        {
            return RedirectToAction("Personal");
        }

        [Route("Starfsferill")]
        public IActionResult Employment()
        {
            ViewData["ContentHeader"] = "Starfsferill";
            ViewData["selectedNavPillId"] = "navPillEmployment";

            var vm = new EmploymentViewModel()
            {
                Employements = new List<Employment>
                {
                    new Employment {Title="Forritari", CompanyName = "Advania", From = DateTime.Now.AddMonths(-8), To = DateTime.Now}
                },
                EmployementsSpouse = new List<Employment>
                {
                    new Employment()
                }
            };

            return View(vm);
        }

        [Route("EmploymentForward")]
        [HttpPost]
        public IActionResult EmploymentForward(PersonalViewModel vm)
        {
            return RedirectToAction("Finances");
        }

        [Route("EmploymentBackwards")]
        [HttpPost]
        public IActionResult EmploymentBackwards(PersonalViewModel vm)
        {
            return RedirectToAction("Education");
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

        private void AddError(string errorMessage)
        {
            ModelState.AddModelError(string.Empty, errorMessage);
        }
    }
}
