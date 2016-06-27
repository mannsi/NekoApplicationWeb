using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using NekoApplicationWeb.ViewModels.Page.Loan;

namespace NekoApplicationWeb.Controllers.api
{
    [Route("api/loan")]
    [Authorize]
    public class LoanController : Controller
    {
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
    }
}
