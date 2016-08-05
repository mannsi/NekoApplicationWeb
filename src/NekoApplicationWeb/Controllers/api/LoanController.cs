using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ServiceInterfaces;
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

        public LoanController(
            ApplicationDbContext dbContext,
            ILoanService loanService,
            IInterestsService interestsService,
            UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _loanService = loanService;
            _interestsService = interestsService;
            _userManager = userManager;
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

            if (!string.IsNullOrEmpty(vm.LenderName))
            {
                var lender = _dbContext.Lenders.FirstOrDefault(l => l.Name == vm.LenderName);
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
        public List<BankLoanViewModel> GetDefaultLoans(string lenderName, string propertyNumber, int buyingPrice, int ownCapital)
        {
            var lender = _dbContext.Lenders.FirstOrDefault(l => l.Name == lenderName);
            if (lender == null) return null;

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

    }
}
