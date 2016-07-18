using System.Collections.Generic;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ViewModels.Page.Loan;

namespace NekoApplicationWeb.ServiceInterfaces
{
    public interface ILoanService
    {
        List<BankLoanViewModel> GetDefaultLoansForLender(string lenderId, string propertyNumber, int buyingPrice, int ownCapital);
    }
}