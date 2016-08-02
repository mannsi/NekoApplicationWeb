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
using ApplicantEmploymentViewModel = NekoApplicationWeb.ViewModels.Page.Employment.ApplicantEmploymentViewModel;

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
        /// <param name="verifyingUserId">User id if coming from a context where a user needs to confirm EULA on this page</param>
        /// <returns></returns>
        [Route("Umsaekjandi")]
        [HttpGet]
        public async Task<IActionResult> Personal(string verifyingUserId)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            var allApplicantsConnections = GetUsersConnectionsForUsersApplication(loggedInUser, _dbContext);
            if (allApplicantsConnections == null)
            {
                return View("Error");
            }

            var viewModelUsers = new List<UserViewModel>();
            foreach (var applicationUserConnection in allApplicantsConnections)
            {
                var vmUser = new UserViewModel(applicationUserConnection.User, applicationUserConnection.UserHasAgreedToEula);
                viewModelUsers.Add(vmUser);
            }

            var application = allApplicantsConnections.First().Application;
            var verifyingUser = _dbContext.Users.FirstOrDefault(u => u.Id == verifyingUserId);
            var verifyingUserHasConfirmedEula = verifyingUser != null &&
                !_dbContext.ApplicationUserConnections.FirstOrDefault(
                    con => con.Application == application && con.User == verifyingUser).UserHasAgreedToEula;

            ViewData["ContentHeader"] = "Umsækjendur";
            ViewData["selectedNavPillId"] = "navPillApplicant";

            var vm = new PersonalViewModel
            {
                ShowEula = verifyingUserHasConfirmedEula,
                EulaUser = verifyingUser,
                Applicants = viewModelUsers
            };

            ViewData["vm"] = vm;
            return View("BasePage", "personal");
        }

        [Route("Menntun")]
        public async Task<IActionResult> Education()
        {
            ViewData["ContentHeader"] = "Menntun";
            ViewData["selectedNavPillId"] = "navPillEducation";

            var vm = new List<ApplicantDegreesViewModel>();
            var loggedInUser = await _userManager.GetUserAsync(User);

            var userConnections = GetUsersConnectionsForUsersApplication(loggedInUser, _dbContext);
            if (userConnections == null) return View("Error");

            foreach (var applicationUserConnection in userConnections)
            {
                var user = applicationUserConnection.User;
                var usersEducationVm = new ApplicantDegreesViewModel {Applicant = user, Degrees = new List<DegreeViewModel>()};

                var usersDegreesFromDb = _dbContext.ApplicantEducations.Where(education => education.User == user);

                foreach (var degreeFomDb in usersDegreesFromDb)
                {
                    usersEducationVm.Degrees.Add(new DegreeViewModel
                    {
                        Id = degreeFomDb.Id,
                        Degree = degreeFomDb.Degree,
                        School = degreeFomDb.School,
                        DateFinished = degreeFomDb.FinishingDate
                    } );

                    vm.Add(usersEducationVm);
                }
            }

            ViewData["vm"] = vm;
            return View("BasePage", "education");
        }

        [Route("Starfsferill")]
        public async Task<IActionResult> Employment()
        {
            ViewData["ContentHeader"] = "Starfsferill";
            ViewData["selectedNavPillId"] = "navPillEmployment";

            var vm = new List<ApplicantEmploymentViewModel>();
            var loggedInUser = await _userManager.GetUserAsync(User);

            var userConnections = GetUsersConnectionsForUsersApplication(loggedInUser, _dbContext);
            if (userConnections == null) return View("Error");

            foreach (var applicationUserConnection in userConnections)
            {
                var user = applicationUserConnection.User;
                var userEmployement = _dbContext.ApplicantEmployments.FirstOrDefault(employment => employment.User == user);
                if (userEmployement == null) return View("Error");

                vm.Add(new ApplicantEmploymentViewModel
                {
                    Applicant = user,
                    CompanyName = userEmployement.Company,
                    From = userEmployement.StartingTime,
                    Title = userEmployement.Title
                });
            }

            ViewData["vm"] = vm;
            return View("BasePage", "employment");
        }

        [Route("Fjarmal")]
        public async Task<IActionResult> Finances()
        {
            ViewData["ContentHeader"] = "Fjármál";
            ViewData["selectedNavPillId"] = "navPillFinances";

            var vm = new FinancesViewModel();

            var loggedInUser = await _userManager.GetUserAsync(User);
            var userConnections = GetUsersConnectionsForUsersApplication(loggedInUser, _dbContext);
            if (userConnections == null) return View("Error");

            // Add income viewmodels
            foreach (var userConnection in userConnections)
            {
                var user = userConnection.User;

                var usersIncomes = _dbContext.ApplicantIncomes.Where(income => income.User == user);

                var incomeViewModel = new IncomeViewModel {Applicant = user};

                // Start with the salary part. It cannot be removed and is handled separately
                var salaryIncome = usersIncomes.First(income => income.IncomeType == IncomeType.Salary);
                incomeViewModel.SalaryIncome = new ApplicantIncomeViewModel
                    {
                        Id = salaryIncome.Id,
                        IncomeType = salaryIncome.IncomeType,
                        MonthlyAmount = salaryIncome.MonthlyAmount
                    };

                // Handle other income
                foreach (var applicantIncome in usersIncomes.Where(income => income.IncomeType != IncomeType.Salary))
                {
                    incomeViewModel.OtherIncomes.Add(new ApplicantIncomeViewModel
                    {
                        Id = applicantIncome.Id,
                        IncomeType = applicantIncome.IncomeType,
                        MonthlyAmount = applicantIncome.MonthlyAmount
                    });
                }

                vm.IncomesViewModel.Add(incomeViewModel);
            }

            var application = userConnections[0].Application;

            // Add assets viewmodels
            var assets = _dbContext.Assets.Where(asset => asset.Application == application);
            foreach (var asset in assets)
            {
                vm.AssetsViewModel.Add(new AssetViewModel
                {
                    Id = asset.Id,
                    AssetNumber = asset.AssetNumber,
                    AssetType = asset.AssetType,
                    AssetWillBeSold = asset.AssetWillBeSold
                });
            }

            // Add debts viewmodels
            var debts = _dbContext.Debts.Where(debt => debt.Application == application);
            foreach (var debt in debts)
            {
                vm.DebtsViewModel.Add(new DebtViewModel
                {
                    Id = debt.Id,
                    DebtType = debt.DebtType,
                    Lender = debt.Lender,
                    LoanRemains = debt.LoanRemains,
                    MonthlyPayment = debt.MonthlyPayment
                });
            }

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

        /// <summary>
        /// Gets user connections for the first application tied to the given user
        /// </summary>
        public static List<ApplicationUserConnection> GetUsersConnectionsForUsersApplication(ApplicationUser user, ApplicationDbContext dbContext)
        {
            if (user == null) return null;

            var loggedInUserApplicationConnection = dbContext.ApplicationUserConnections.Include(connection => connection.Application).FirstOrDefault(auc => auc.User == user);
            if (loggedInUserApplicationConnection == null) return null;

            var application = loggedInUserApplicationConnection.Application;
            var allApplicantsConnections = dbContext.ApplicationUserConnections.Include(con => con.User).Where(auc => auc.Application == application).OrderBy(con => con.User.UserName).ToList();

            return allApplicantsConnections;
        }
    }
}
