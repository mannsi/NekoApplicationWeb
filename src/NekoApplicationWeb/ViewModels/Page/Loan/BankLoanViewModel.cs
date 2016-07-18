using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.ViewModels.Page.Loan
{
    public class BankLoanViewModel
    {
        public int Principal { get; set; }
        public int LoanDurationYears { get; set; }
        public int LoanDurationMaxYears { get; set; }
        public int LoanDurationMinYears { get; set; }
        public InterestsInfo InterestInfo { get; set; }
        public int MonthlyPayment { get; set; }

    }
}


