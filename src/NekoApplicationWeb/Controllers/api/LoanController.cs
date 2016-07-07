using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ViewModels.Page.Loan;

namespace NekoApplicationWeb.Controllers.api
{
    [Route("api/loan")]
    [Authorize]
    public class LoanController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public LoanController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
        public List<BankLoanViewModel> GetDefaultLoans(string lenderId, int buyingPrice, int ownCapital)
        {
            // TODO calculations
            // TODO needs fasteignarmat

            var selectedLender = _dbContext.Lenders.FirstOrDefault(lender => lender.Id == lenderId);
            if (selectedLender == null) return null;

            
            var loan1 = new BankLoanViewModel
            {
                Principal = (int)(buyingPrice * 0.5) - ownCapital,
                Indexed = false,
                LoanDurationYears = 40,
                LoanDurationMaxYears = 40,
                LoanDurationMinYears = 15
            };

            var loan2 = new BankLoanViewModel
            {
                Principal = (int)(buyingPrice * 0.7) - loan1.Principal,
                Indexed = true,
                LoanDurationYears = 40,
                LoanDurationMaxYears = 40,
                LoanDurationMinYears = 15
            };

            var loan3 = new BankLoanViewModel
            {
                Principal = (int)(buyingPrice * 0.85) - loan1.Principal - loan2.Principal,
                Indexed = true,
                LoanDurationYears = 15,
                LoanDurationMaxYears = 15,
                LoanDurationMinYears = 15
            };

            return new List<BankLoanViewModel> {loan1, loan2, loan3};
        }

    }
}
