using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using NekoApplicationWeb.ViewModels;
using NekoApplicationWeb.ViewModels.Page;
using NekoApplicationWeb.ViewModels.Page.Education;
using NekoApplicationWeb.ViewModels.Page.Employment;
using NekoApplicationWeb.ViewModels.Page.Finances;
using NekoApplicationWeb.ViewModels.Page.Personal;

namespace NekoApplicationWeb.Controllers
{
    [Route("")]
    [Authorize]
    public class PageController : Controller
    {
        [Route("Umsokn")]
        public IActionResult Index()
        {
            ViewData["ContentHeader"] = "Umsókn um Neko fasteignalán";
            ViewData["selectedNavPillId"] = "navPillFrontPage";
            ViewData["vm"] = null;
            return View("BasePage", "Index");
        }

        [Route("Umsaekjandi")]
        [HttpGet]
        public IActionResult Personal()
        {
            ViewData["ContentHeader"] = "Umsækjendur";
            ViewData["selectedNavPillId"] = "navPillApplicant";

            var vm = new List<ApplicantViewModel>
            {
                new ApplicantViewModel
                {
                    Email = "test@testEmail.com",
                    Name = "Mark",
                    Ssn = "1234567899",
                    FacebookPath = "facebook.com/TheZuck"
                }
            };

            ViewData["vm"] = vm;
            return View("BasePage", "Personal");
        }

        [Route("Menntun")]
        public IActionResult Education()
        {
            ViewData["ContentHeader"] = "Menntun";
            ViewData["selectedNavPillId"] = "navPillEducation";

            var vm = new List<ApplicantDegreesViewModel>
            {
                new ApplicantDegreesViewModel
                {
                    ApplicantName = "Joe Smoe",
                    Degrees = new List<DegreeViewModel>
                    {
                        new DegreeViewModel { School = "Háskólinn í Reykjavík", Degree = "MSc í Tölvunarfræði", DateFinished = new DateTime(2018, 6, 1) },
                        new DegreeViewModel { School = "Háskólinn í Reykjavík", Degree = "BS í Tölvunarfræði", DateFinished = new DateTime(2015, 6, 1) }
                    }
                },
                new ApplicantDegreesViewModel
                {
                    ApplicantName = "Ms Joe Smoe",
                    Degrees = new List<DegreeViewModel>
                    {
                        new DegreeViewModel { School = "Háskólinn í Reykjavík", Degree = "Arts and crafts", DateFinished = new DateTime(2016, 6, 1) }
                    }
                }
            };

            ViewData["vm"] = vm;
            return View("BasePage", "Education");
        }

       [Route("Starfsferill")]
        public IActionResult Employment()
        {
            ViewData["ContentHeader"] = "Starfsferill";
            ViewData["selectedNavPillId"] = "navPillEmployment";

           var vm = new List<ApplicantEmployment>
           {
               new ApplicantEmployment { ApplicantName =  "Joe smoe", Title = "Forritari", CompanyName = "Advania", From = DateTime.Now.AddMonths(-8)},
               new ApplicantEmployment { ApplicantName =  "Ms Joe smoe", Title = "Stjórnandi", CompanyName = "Þjóðleikhúsið", From = DateTime.Now.AddMonths(-6)}
           };

            ViewData["vm"] = vm;
            return View("BasePage", "Employment");
        }

        [Route("Fjarmal")]
        public IActionResult Finances()
        {
            ViewData["ContentHeader"] = "Fjármál";
            ViewData["selectedNavPillId"] = "navPillFinances";

            var vm = new List<ApplicantFinancesViewModel>
            {
                new ApplicantFinancesViewModel
                {
                    ApplicantName = "Joe Shmoe",
                    MonthlyPay = new ApplicantIncome {IncomeType = IncomeType.Salary, MonthlyAmount = 500000},
                    Assets = new List<ApplicantAssets>(),
                    Debts =new List<ApplicantDebt>(),
                    OtherIcome = new List<ApplicantIncome>()
                }, 
                new ApplicantFinancesViewModel
                {
                    ApplicantName = "Ms Joe Shmoe",
                    MonthlyPay = new ApplicantIncome {IncomeType = IncomeType.Salary, MonthlyAmount = 500000},
                    Assets = new List<ApplicantAssets>(),
                    Debts =new List<ApplicantDebt>(),
                    OtherIcome = new List<ApplicantIncome>()
                }
            };

            ViewData["vm"] = vm;
            return View("BasePage", "Finances");
        }

        [Route("Lanveiting")]
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

        //[Route("SummaryBackwards")]
        //[HttpPost]
        //public IActionResult SummaryBackwards(PersonalViewModel vm)
        //{
        //    return RedirectToAction("Documents");
        //}

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
