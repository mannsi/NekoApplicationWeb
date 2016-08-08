using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ILenderService _lenderService;

        public LoanController(
            ApplicationDbContext dbContext,
            ILoanService loanService,
            IInterestsService interestsService,
            UserManager<ApplicationUser> userManager,
            ICostOfLivingService costOfLivingService,
            ILenderService lenderService)
        {
            _dbContext = dbContext;
            _loanService = loanService;
            _interestsService = interestsService;
            _userManager = userManager;
            _costOfLivingService = costOfLivingService;
            _lenderService = lenderService;
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

        [Route("defaultLoans")]
        [HttpGet]
        public async Task<DefaultLoansViewModel> GetDefaultLoans(string lenderId, string propertyNumber, int buyingPrice, int ownCapital)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);
            var thjodskraUser = _dbContext.ThjodskraPersons.First(person => person.Id == loggedInUser.Id);
            var application = PageController.GetApplicationForUser(loggedInUser, _dbContext);
            var lender = _dbContext.Lenders.FirstOrDefault(l => l.Id == lenderId);
            if (lender == null) return null;
            var loans = GetLoans(lender, buyingPrice, ownCapital);
            var loansTotalAmount = loans.Sum(loan => loan.Principal);

            // Ratios
            double vedsetningarHlutfall = 100.0*(loansTotalAmount - ownCapital)/loansTotalAmount;
            var totalIncome = application.TotalMonthlyIncomeForAllApplicant;
            var totalLoanPayments = loans.Sum(loan => loan.MonthlyPayment);
            var estimatedCostOfLiving = GetCostOfLivingWithoutLoans(thjodskraUser.FamilyNumber, application, buyingPrice);
            var greidslugeta = totalIncome - totalLoanPayments - estimatedCostOfLiving;

            // Lender rules
            var lenderRule = _lenderService.VerifyLenderRules(lender, totalIncome, totalLoanPayments);
            
            // Neko minimum loan
            bool needsNekoLoan = ownCapital < buyingPrice * 0.15;

            var vm = new DefaultLoansViewModel
            {
                DefaultLoans = loans,
                Vedsetningarhlutfall = vedsetningarHlutfall,
                IsVedsetningarhlutfallOk = true,
                Greidslugeta = greidslugeta,
                IsGreidslugetaOk = 0 < greidslugeta,
                Skuldahlutfall = 0,
                IsSkuldahlutfallOk = true,
                LenderLendingRulesBroken = lenderRule.RulesBroken,
                LenderLendingRulesBrokenText = lenderRule.RulesBrokenText,
                NeedsNekoLoan = needsNekoLoan,
                LenderNameThagufall = lender.NameThagufall
            };

            return vm;
        }


        private List<BankLoanViewModel> GetLoans(Lender lender, int buyingPrice, int ownCapital)
        {
           var interestsForLender = _interestsService.GetInterestsMatrix(lender);

            // TODO fetch these values from some service
            int realEstateValuation = 25000000;
            int newFireInsuranceValuation = 23000000;
            int plotAssessmentValue = 3500000;

            return _loanService.GetDefaultLoansForLender(
                lender,
                buyingPrice,
                ownCapital,
                interestsForLender,
                realEstateValuation,
                newFireInsuranceValuation,
                plotAssessmentValue);
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
            int numberOfAdults = familyMembers.Count(member => 17 < member.Ssn.SsnToAge(DateTime.Now));
            int numberOfPreSchoolKids = familyMembers.Count(member => 0 < member.Ssn.SsnToAge(DateTime.Now) && member.Ssn.SsnToAge(DateTime.Now) < 6);
            int numberOfElementarySchoolKids = familyMembers.Count(member => 6 <= member.Ssn.SsnToAge(DateTime.Now) && member.Ssn.SsnToAge(DateTime.Now) <= 17);
            return _costOfLivingService.GetCostOfLivingWithoutLoans(costOfLivingEntries, numberOfAdults, numberOfPreSchoolKids, numberOfElementarySchoolKids, numberOfCars, buyingPrice);
        }
    }
}
