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
    public class ApplicationController : Controller
    {
        [Route("Umsokn")]
        public IActionResult Index()
        {
            ViewData["ContentHeader"] = "Umsókn um Neko fasteignalán";
            ViewData["vm"] = null;
            return View("BasePage", "Index");
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

            ViewData["vm"] = vm;
            return View("BasePage", "Personal");
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
            ViewData["vm"] = vm;
            return View("BasePage", "Education");
        }

        [Route("EducationForward")]
        [HttpPost]
        public IActionResult EducationForward(EducationViewModel vm)
        {
            return RedirectToAction("Employment");
        }

        [Route("EducationBackwards")]
        [HttpPost]
        public IActionResult EducationBackwards(EducationViewModel vm)
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

            ViewData["vm"] = vm;
            return View("BasePage", "Employment");
        }

        [Route("EmploymentForward")]
        [HttpPost]
        public IActionResult EmploymentForward(EmploymentViewModel vm)
        {
            return RedirectToAction("Finances");
        }

        [Route("EmploymentBackwards")]
        [HttpPost]
        public IActionResult EmploymentBackwards(EmploymentViewModel vm)
        {
            return RedirectToAction("Education");
        }

        [Route("Fjarmal")]
        public IActionResult Finances()
        {
            ViewData["ContentHeader"] = "Fjármál";
            ViewData["selectedNavPillId"] = "navPillFinances";

            var vm = new FinancesViewModel()
            {
                MonthlyPayPreTax = 500000,
                MonthlyPayPreTaxSpouse = 400000,
                OtherLoans = new List<OtherLoan>()
                {
                    new OtherLoan() { Remains = 1000000, MonthlyPayment = 25000}      
                }
            };

            ViewData["vm"] = vm;
            return View("BasePage", "Finances");
        }

        [Route("FinancesForward")]
        [HttpPost]
        public IActionResult FinancesForward(FinancesViewModel vm)
        {
            return RedirectToAction("Loan");
        }

        [Route("FinancesBackwards")]
        [HttpPost]
        public IActionResult FinancesBackwards(FinancesViewModel vm)
        {
            return RedirectToAction("Employment");
        }

        [Route("Lan")]
        public IActionResult Loan()
        {
            ViewData["ContentHeader"] = "Lán";
            ViewData["selectedNavPillId"] = "navPillLoan";

            var vm = new LoanViewModel
            {
                BuyingPrice = 22000000,
                PropertyNumber = "123-1234",
                NekoLoanAmount = 2500000,
                BankLoans = new List<BankLoan>
                {
                    new BankLoan {MonthlyPayment = 190000, Principal = 19500000 }
                }
            };

            ViewData["vm"] = vm;
            return View("BasePage", "Loan");
        }

        [Route("LoanForward")]
        [HttpPost]
        public IActionResult LoanForward(LoanViewModel vm)
        {
            return RedirectToAction("Documents");
        }

        [Route("LoanBackwards")]
        [HttpPost]
        public IActionResult LoanBackwards(LoanViewModel vm)
        {
            return RedirectToAction("Finances");
        }

        [Route("Fylgiskjol")]
        public IActionResult Documents()
        {
            ViewData["ContentHeader"] = "Fylgiskjöl";
            ViewData["selectedNavPillId"] = "navPillDocuments";

            var vm = new DocumentsViewModel
            {
                
            };

            ViewData["vm"] = vm;
            return View("BasePage", "Documents");
        }

        [Route("DocumentsForward")]
        [HttpPost]
        public IActionResult DocumentsForward(DocumentsViewModel vm)
        {
            return RedirectToAction("Summary");
        }

        [Route("DocumentsBackwards")]
        [HttpPost]
        public IActionResult DocumentsBackwards(DocumentsViewModel vm)
        {
            return RedirectToAction("Loan");
        }

        [Route("Samatekt")]
        public IActionResult Summary()
        {
            ViewData["ContentHeader"] = "Samatekt";
            ViewData["selectedNavPillId"] = "navPillSummary";

            ViewData["vm"] = null;
            return View("BasePage", "Summary");
        }

        [Route("SummaryBackwards")]
        [HttpPost]
        public IActionResult SummaryBackwards(PersonalViewModel vm)
        {
            return RedirectToAction("Documents");
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
