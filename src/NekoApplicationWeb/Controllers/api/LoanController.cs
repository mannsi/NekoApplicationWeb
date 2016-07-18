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

        public LoanController(
            ApplicationDbContext dbContext,
            ILoanService loanService)
        {
            _dbContext = dbContext;
            _loanService = loanService;
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
            return _loanService.GetDefaultLoansForLender(lenderId, propertyNumber, buyingPrice, ownCapital);
        }

    }
}
