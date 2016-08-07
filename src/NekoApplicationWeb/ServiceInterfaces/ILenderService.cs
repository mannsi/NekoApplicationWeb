using System.Collections.Generic;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ViewModels.Page.Loan;

namespace NekoApplicationWeb.ServiceInterfaces
{
    public interface ILenderService
    {
        LenderLendingRule VerifyLenderRules(Lender lender, int totalApplicantMonthlyIncome, int totalLoanPayments);
    }
}