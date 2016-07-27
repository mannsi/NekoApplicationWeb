using System.Collections.Generic;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ViewModels.Page.Loan;

namespace NekoApplicationWeb.ServiceInterfaces
{
    public interface ILoanService
    {
        List<BankLoanViewModel> GetDefaultLoansForLender(
            Lender lender,
            int buyingPrice,
            int ownCapital,
            List<InterestsEntry> interestLinesForLender,
            int realEstateValuation,
            int newFireInsuranceValuation,
            int plotAssessmentValue);
    }
}