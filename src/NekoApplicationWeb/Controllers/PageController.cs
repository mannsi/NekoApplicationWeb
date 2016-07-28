using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ServiceInterfaces;
using NekoApplicationWeb.ViewModels;
using NekoApplicationWeb.ViewModels.Page;
using NekoApplicationWeb.ViewModels.Page.Education;
using NekoApplicationWeb.ViewModels.Page.Finances;
using NekoApplicationWeb.ViewModels.Page.Loan;
using NekoApplicationWeb.ViewModels.Page.Personal;
using NekoApplicationWeb.ViewModels.Page.Start;
using ApplicantEmployment = NekoApplicationWeb.ViewModels.Page.Employment.ApplicantEmployment;

namespace NekoApplicationWeb.Controllers
{
    [Route("")]
    [Authorize]
    public class PageController : Controller
    {
        private readonly IThjodskraService _thjodskraService;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public PageController(IThjodskraService thjodskraService,
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager)
        {
            _thjodskraService = thjodskraService;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [Route("Start")]
        public async Task<IActionResult> Start()
        {
            var loggedInUser = await _userManager.GetUserAsync(User);
            if (loggedInUser == null) return View("Error");

            var userApplicationConnection = _dbContext.ApplicationUserConnections.FirstOrDefault(auc => auc.User == loggedInUser);
            if (userApplicationConnection == null) return View("Error");

            var vm = new StartPageViewModel
            {
                ShowEula = !userApplicationConnection.UserHasAgreedToEula,
                EulaUser = loggedInUser
            };

            ViewData["ContentHeader"] = "Umsókn um Neko fasteignalán";
            ViewData["selectedNavPillId"] = "navPillFrontPage";
            ViewData["vm"] = vm;
            return View("BasePage", "Start");
        }

        /// <summary>
        /// Personal info on the applicants
        /// </summary>
        /// <param name="verifyingUser">User if coming from a context where a user needs to confirm EULA on this page</param>
        /// <returns></returns>
        [Route("Umsaekjandi")]
        [HttpGet]
        public async Task<IActionResult> Personal(ApplicationUser verifyingUser = null)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);
            if (loggedInUser == null) return View("Error");

            var loggedInUserApplicationConnection = _dbContext.ApplicationUserConnections.Include(connection => connection.Application).FirstOrDefault(auc => auc.User == loggedInUser);
            if (loggedInUserApplicationConnection == null) return View("Error");

            // Just to make sure the logged in user comes first
            var viewModelUsers = new List<UserViewModel>();
            viewModelUsers.Add(new UserViewModel(loggedInUser, loggedInUserApplicationConnection.UserHasAgreedToEula));

            var application = loggedInUserApplicationConnection.Application;
            var allApplicantsConnections = _dbContext.ApplicationUserConnections.Include(con => con.User).Where(auc => auc.Application == application).ToList();

            foreach (var applicationUserConnection in allApplicantsConnections.Where(con => con.User != loggedInUser))
            {
                var vmUser = new UserViewModel(applicationUserConnection.User, applicationUserConnection.UserHasAgreedToEula);
                viewModelUsers.Add(vmUser);
            }

            ViewData["ContentHeader"] = "Umsækjendur";
            ViewData["selectedNavPillId"] = "navPillApplicant";

            var vm = new PersonalViewModel
            {
                ShowEula = (verifyingUser != null),
                EulaUser = verifyingUser,
                Applicants = viewModelUsers
            };

            //var vm = new List<ApplicantViewModel>
            //{
            //    new ApplicantViewModel
            //    {
            //        Email = "test@testEmail.com",
            //        Name = "Mark",
            //        Ssn = "1234567899",
            //        FacebookPath = "facebook.com/TheZuck"
            //    }
            //};

            ViewData["vm"] = vm;
            return View("BasePage", "personal");
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
            return View("BasePage", "education");
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
            return View("BasePage", "employment");
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
                    ApplicantSsn = "111111-9999",
                    ApplicantName = "Joe Shmoe",
                    MonthlyPay = new ApplicantFinancesIncome {IncomeType = IncomeType.Salary, MonthlyAmount = 500000},
                    Assets = new List<ApplicantFinancesAsset>(),
                    Debts =new List<ApplicantFinancesDebt>(),
                    OtherIcome = new List<ApplicantFinancesIncome>()
                }, 
                new ApplicantFinancesViewModel
                {
                    ApplicantSsn = "111112-9999",
                    ApplicantName = "Ms Joe Shmoe",
                    MonthlyPay = new ApplicantFinancesIncome {IncomeType = IncomeType.Salary, MonthlyAmount = 500000},
                    Assets = new List<ApplicantFinancesAsset>(),
                    Debts =new List<ApplicantFinancesDebt>(),
                    OtherIcome = new List<ApplicantFinancesIncome>()
                }
            };

            ViewData["vm"] = vm;
            return View("BasePage", "finances");
        }

        [Route("Lanveiting")]
        public IActionResult Loan()
        {
            ViewData["ContentHeader"] = "Lánveiting";
            ViewData["selectedNavPillId"] = "navPillLoan";

            var vm = new LoanViewModel
            {
                BuyingPrice = 25000000,
                OwnCapital = 1000000,
                BankLoans = new List<BankLoanViewModel>()
            };

            ViewData["vm"] = vm;
            return View("BasePage", "Loan");
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
            return View("BasePage", "documents");
        } 

        [Route("Samatekt")]
        public IActionResult Summary()
        {
            ViewData["ContentHeader"] = "Samatekt";
            ViewData["selectedNavPillId"] = "navPillSummary";

            ViewData["vm"] = null;
            return View("BasePage", "summary");
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
