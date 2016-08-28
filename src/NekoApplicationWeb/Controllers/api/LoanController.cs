using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ServiceInterfaces;
using NekoApplicationWeb.Shared;
using NekoApplicationWeb.ViewModels.Page.Loan;

namespace NekoApplicationWeb.Controllers.api
{
    [Route("api/loan")]
    [Authorize]
    public class LoanController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILoanService _loanService;
        private readonly IInterestsService _interestsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICostOfLivingService _costOfLivingService;
        private readonly IPropertyValuationService _propertyValuationService;
        private readonly ILogger<LoanController> _logger;
        private readonly ICompletionService _completionService;

        public LoanController(
            ApplicationDbContext dbContext,
            ILoanService loanService,
            IInterestsService interestsService,
            UserManager<ApplicationUser> userManager,
            ICostOfLivingService costOfLivingService,
            IPropertyValuationService propertyValuationService,
            ILogger<LoanController> logger,
            ICompletionService completionService)
        {
            _dbContext = dbContext;
            _loanService = loanService;
            _interestsService = interestsService;
            _userManager = userManager;
            _costOfLivingService = costOfLivingService;
            _propertyValuationService = propertyValuationService;
            _logger = logger;
            _completionService = completionService;
        }

        [Route("")]
        [HttpPost]
        public async Task Save([FromBody]LoanViewModel vm)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);
            var application = PageController.GetApplicationForUser(loggedInUser, _dbContext);
            var propertyDetails = _dbContext.PropertyDetails.FirstOrDefault(detail => detail.Application == application);

            if (propertyDetails == null) return;

            _dbContext.Update(propertyDetails);
            propertyDetails.BuyingPrice = vm.BuyingPrice;
            propertyDetails.OwnCapital = vm.OwnCapital;
            propertyDetails.PropertyNumber = vm.PropertyNumber;

            if (!string.IsNullOrEmpty(vm.LenderId))
            {
                var lender = _dbContext.Lenders.FirstOrDefault(l => l.Id == vm.LenderId);
                if (lender == null) return;

                if (application.Lender != lender)
                {
                    _dbContext.Update(application);
                    application.Lender = lender;
                }
            }
        
            _dbContext.SaveChanges();

            // Update the completion status of the application
            application.LoanPageCompleted = _completionService.LoanCompleted(User);
            _dbContext.Update(application);
            _dbContext.SaveChanges();
        }

        [Route("new")]
        [HttpGet]
        public BankLoanViewModel NewBankLoan()
        {
            return new BankLoanViewModel();
        }

        [Route("lenders")]
        [HttpGet]
        public List<Lender> Lenders()
        {
            return _dbContext.Lenders.Where(lender => lender.Id != Shared.Constants.NekoLenderId).ToList();
        }

        [Route("propertyValid")]
        [HttpGet]
        public PropertyNumberViewModel PropertyValid(string propertyNumber)
        {
            var vm = new PropertyNumberViewModel();
            vm.PropertyNumberOk = true;

            var propertyValuation = _propertyValuationService.GetPropertyValuation(propertyNumber);

            if (propertyValuation == null)
            {
                vm.PropertyNumberOk = false;
                vm.PropertyNumberProblem = "Þessi eign er ekki á skrá hjá okkur. Við lánum eingöngu fyrir eignum undir 150 fm í fjölbýli.";
            }
            else if (propertyValuation.NewFireInsuranceValuation == 0 || 
                propertyValuation.PlotAssessmentValue == 0 ||
                propertyValuation.RealEstateValuation2017 == 0)
            {
                vm.PropertyNumberOk = false;
                vm.PropertyNumberProblem = "Ónóg gögn um eignina fundust hjá Þjóðskrá";
            }

            return vm;
        }

        [Route("defaultLoans")]
        [HttpGet]
        public async Task<DefaultLoansViewModel> GetDefaultLoans(string lenderId, string propertyNumber, int buyingPrice, int ownCapital)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);
            var thjodskraUser = _dbContext.ThjodskraPersons.First(person => person.Id == loggedInUser.Id);
            var application = PageController.GetApplicationForUser(loggedInUser, _dbContext);
            var lender = _dbContext.Lenders.FirstOrDefault(l => l.Id == lenderId);
            if (lender == null) return null;
            var loans = GetLoans(lender, buyingPrice, ownCapital, propertyNumber);
            var loansTotalAmount = loans.Sum(loan => loan.Principal);

            // Ratios
            double vedsetningarHlutfall = 100.0*(loansTotalAmount - ownCapital)/loansTotalAmount;
            var totalIncome = application.TotalMonthlyIncomeForAllApplicant;
            var totalLoanPayments = loans.Sum(loan => loan.MonthlyPayment);
            var estimatedCostOfLiving = GetCostOfLivingWithoutLoans(thjodskraUser.FamilyNumber, application, buyingPrice);
            var greidslugeta = totalIncome - totalLoanPayments - estimatedCostOfLiving;
            var greidslubyrdarhlutfall = (100.0 *totalLoanPayments)/totalIncome;

            // Lender rules
            bool greidslubyrdarhlutfallOk = true;
            bool lenderRulesBroken = false;
            string lenderRulesBrokenText = "";
            var maxGreidslubyrdarhlutfall = lender.MaxDebtServiceToIncome;
            if (maxGreidslubyrdarhlutfall < greidslubyrdarhlutfall)
            {
                greidslubyrdarhlutfallOk = false;
                lenderRulesBroken = true;
                lenderRulesBrokenText =
                    $"{lender.Name} veitir ekki lán ef greiðslur af lánum eru yfir {lender.MaxDebtServiceToIncome}% af tekjum";
            }

            // Neko minimum loan
            bool needsNekoLoan = ownCapital < buyingPrice * 0.15;

            var vm = new DefaultLoansViewModel
            {
                DefaultLoans = loans,
                Vedsetningarhlutfall = vedsetningarHlutfall,
                IsVedsetningarhlutfallOk = true,
                Greidslugeta = greidslugeta,
                IsGreidslugetaOk = 0 < greidslugeta,
                Greidslubyrdarhlutfall = greidslubyrdarhlutfall,
                IsGreidslubyrdarhlutfallOk = greidslubyrdarhlutfallOk,
                LenderLendingRulesBroken = lenderRulesBroken,
                LenderLendingRulesBrokenText = lenderRulesBrokenText,
                NeedsNekoLoan = needsNekoLoan,
                LenderNameThagufall = lender.NameThagufall
            };

            return vm;
        }

        private List<BankLoanViewModel> GetLoans(Lender lender, int buyingPrice, int ownCapital, string propertyNumber)
        {
           var interestsForLender = _interestsService.GetInterestsMatrix(lender);

            var propertyValuation = _propertyValuationService.GetPropertyValuation(propertyNumber);
            if (propertyValuation == null)
            {
                return null;
            }

            return _loanService.GetDefaultLoansForLender(
                lender,
                buyingPrice,
                ownCapital,
                interestsForLender,
                propertyValuation.RealEstateValuation2017,
                propertyValuation.NewFireInsuranceValuation,
                propertyValuation.PlotAssessmentValue);
        }

        private int GetCostOfLivingWithoutLoans(string familyNumber, Application application, int buyingPrice)
        {
            var numberOfCars =
                _dbContext.Assets.Count(
                    asset =>
                        !asset.AssetWillBeSold && asset.AssetType == AssetType.Vehicle &&
                        asset.Application == application);

            var costOfLivingEntries = _dbContext.CostOfLivingEntries.ToList();
            var familyMembers = _dbContext.ThjodskraFamilyEntries.Where(entry => entry.FamilyNumber == familyNumber).ToList();

            foreach (var thjodskraFamilyEntry in familyMembers)
            {
                _logger.LogWarning($"{thjodskraFamilyEntry.Name}: SSN is '{thjodskraFamilyEntry.Ssn}' and I think the age is '{thjodskraFamilyEntry.Ssn.SsnToAge(DateTime.Now)}'");
            }

            int numberOfAdults = familyMembers.Count(member => 17 < member.Ssn.SsnToAge(DateTime.Now));
            int numberOfPreSchoolKids = familyMembers.Count(member => 0 < member.Ssn.SsnToAge(DateTime.Now) && member.Ssn.SsnToAge(DateTime.Now) < 6);
            int numberOfElementarySchoolKids = familyMembers.Count(member => 6 <= member.Ssn.SsnToAge(DateTime.Now) && member.Ssn.SsnToAge(DateTime.Now) <= 17);
            return _costOfLivingService.GetCostOfLivingWithoutLoans(costOfLivingEntries, numberOfAdults, numberOfPreSchoolKids, numberOfElementarySchoolKids, numberOfCars, buyingPrice);
        }
    }
}
