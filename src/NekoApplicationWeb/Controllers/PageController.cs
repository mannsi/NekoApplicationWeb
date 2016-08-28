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
using Microsoft.Extensions.Logging;
using NekoApplicationWeb.Controllers.api;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ServiceInterfaces;
using NekoApplicationWeb.Shared;
using NekoApplicationWeb.ViewModels;
using NekoApplicationWeb.ViewModels.Page;
using NekoApplicationWeb.ViewModels.Page.Education;
using NekoApplicationWeb.ViewModels.Page.Finances;
using NekoApplicationWeb.ViewModels.Page.Loan;
using NekoApplicationWeb.ViewModels.Page.Personal;
using NekoApplicationWeb.ViewModels.Page.Summary;
using ApplicantEmploymentViewModel = NekoApplicationWeb.ViewModels.Page.Employment.ApplicantEmploymentViewModel;

namespace NekoApplicationWeb.Controllers
{
    [Route("")]
    [Authorize]
    public class PageController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IInterestsService _interestsService;
        private readonly ILoanService _loanService;
        private readonly IApplicationService _applicationService;
        private readonly ICostOfLivingService _costOfLivingService;
        private readonly IPropertyValuationService _propertyValuationService;
        private readonly ILogger<LoanController> _logger;
        private readonly ICompletionService _completionService;

        public PageController(ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            IInterestsService interestsService,
            ILoanService loanService,
            IApplicationService applicationService,
            ICostOfLivingService costOfLivingService,
            IPropertyValuationService propertyValuationService,
            ILogger<LoanController> logger,
            ICompletionService completionService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _interestsService = interestsService;
            _loanService = loanService;
            _applicationService = applicationService;
            _costOfLivingService = costOfLivingService;
            _propertyValuationService = propertyValuationService;
            _logger = logger;
            _completionService = completionService;
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

            var verifyingUser = _dbContext.Users.FirstOrDefault(u => u.Id == verifyingUserId);
            var verifyingUserHasConfirmedEula = verifyingUser != null &&
                !allApplicantsConnections.First(con => con.User == verifyingUser).UserHasAgreedToEula;

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
                }

                vm.Add(usersEducationVm);
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
        public async Task<IActionResult> Loan()
        {
            ViewData["ContentHeader"] = "Lánveiting";
            ViewData["selectedNavPillId"] = "navPillLoan";

            var loggedInUser = await _userManager.GetUserAsync(User);
            var application = GetApplicationForUser(loggedInUser, _dbContext);

            var vm = new LoanViewModel();

            // Populate property details
            var propertyDetails = _dbContext.PropertyDetails.FirstOrDefault(detail => detail.Application == application);

            if (propertyDetails == null) return View("Error");

            vm.BuyingPrice = propertyDetails.BuyingPrice;
            vm.OwnCapital = propertyDetails.OwnCapital;
            vm.PropertyNumber = propertyDetails.PropertyNumber;

            // Populate lender and loan info
            var lender = application.Lender;
            if (lender != null)
            {
                vm.LenderId = lender.Id;
            }

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

        [Route("Nidurstodur")]
        public async Task<IActionResult> Summary()
        {
            ViewData["ContentHeader"] = "Bráðabirgðarniðurstaða";
            ViewData["selectedNavPillId"] = "navPillSummary";

            var vm = new SummaryPageViewModel();

            var application = _applicationService.ActiveApplication(User);
            if (!application.EducationPageCompleted)
            {
                vm.ListOfErrorMessage.Add("Menntun síðan er ekki útfyllt");
            }
            if (!application.PersonalPageCompleted)
            {
                vm.ListOfErrorMessage.Add("Umsækjendur síðan er ekki útfyllt");
            }
            if (!application.EmploymentPageCompleted)
            {
                vm.ListOfErrorMessage.Add("Starfsferill síðan er ekki útfyllt");
            }
            if (!application.FinancesPageCompleted)
            {
                vm.ListOfErrorMessage.Add("Fjármál síðan er ekki útfyllt");
            }
            if (!application.LoanPageCompleted)
            {
                vm.ListOfErrorMessage.Add("Lánveiting síðan er ekki útfyllt");
            }

            if (!vm.ListOfErrorMessage.Any())
            {
                var lender = application.Lender;
                var propertyDetails = _dbContext.PropertyDetails.FirstOrDefault(detail => detail.Application == application);

                var loggedInUser = await _userManager.GetUserAsync(User);

                var loanController = new LoanController(_dbContext, _loanService, _interestsService, _userManager, _costOfLivingService, _propertyValuationService, _logger, _completionService);
                var loanInfo = loanController.GetDefaultLoans(lender.Id, propertyDetails.PropertyNumber, propertyDetails.BuyingPrice,
                    propertyDetails.OwnCapital, loggedInUser);

                if (loanInfo.LenderLendingRulesBroken)
                {
                    vm.ListOfErrorMessage.Add(loanInfo.LenderLendingRulesBrokenText);
                }
                if (!loanInfo.IsGreidslugetaOk)
                {
                    vm.ListOfErrorMessage.Add("Þú hefur því miður ekki greiðslugetu fyrir lánunum");
                }
                if (!loanInfo.NeedsNekoLoan)
                {
                    vm.ListOfErrorMessage.Add("Þú átt svo mikið eigið fé að þú þarft ekki á Neko láni að halda !");
                }
            }

            ViewData["vm"] = vm;

            return View("BasePage", "Summary");
        }

        [Route("SaekjaUm")]
        [HttpPost]
        public IActionResult SaekjaUm()
        {
            // TODO call the applyForLoanService
            // TODO display the 'Umsókn móttekin. Glæsilegt ...' page

            return View("BasePage", "Congratulations");
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

        public static Application GetApplicationForUser(ApplicationUser user, ApplicationDbContext dbContext)
        {
            if (user == null) return null;

            var loggedInUserApplicationConnection = 
                dbContext.ApplicationUserConnections.
                Include(connection => connection.Application).
                ThenInclude(application => application.Lender).
                FirstOrDefault(auc => auc.User == user);

            return loggedInUserApplicationConnection?.Application;
        }

    }
}
