using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
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

        public LoanController(
            ApplicationDbContext dbContext,
            ILoanService loanService,
            IInterestsService interestsService)
        {
            _dbContext = dbContext;
            _loanService = loanService;
            _interestsService = interestsService;
        }

        [Route("")]
        [HttpPost]
        public void Save([FromBody]LoanViewModel vm)
        {
            // TODO save the vm
        }

        [Route("new")]
        [HttpGet]
        public BankLoanViewModel NewBankLoan()
        {
            return new BankLoanViewModel();
        }

        [Route("lenders")]
        [HttpGet]
        public List<Lender> GetLenders()
        {
            return _dbContext.Lenders.ToList();
        }

        [Route("defaultLoans")]
        [HttpGet]
        public List<BankLoanViewModel> GetDefaultLoans(string lenderId, string propertyNumber, int buyingPrice, int ownCapital)
        {
            var lender = _dbContext.Lenders.FirstOrDefault(l => l.Id == lenderId);
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
