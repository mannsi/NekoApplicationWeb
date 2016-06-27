using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.ViewModels.Page.Loan
{
    public class BankLoanViewModel
    {
        public string Lender { get; set; }
        public int Principal { get; set; }
        public int MonthlyPayment { get; set; }
        public bool Indexed { get; set; }
    }
}
