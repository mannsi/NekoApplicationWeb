using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.ViewModels.Page.Loan
{
    public class DefaultLoansViewModel
    {
        public DefaultLoansViewModel()
        {
            DefaultLoans = new List<BankLoanViewModel>();
        }

        public List<BankLoanViewModel> DefaultLoans { get; set; }
        public int Greidslugeta { get; set; }
        public double Skuldahlutfall { get; set; }
        public double Vedsetningarhlutfall { get; set; }

        public bool IsGreidslugetaOk { get; set; }
        public bool IsSkuldahlutfallOk { get; set; }
        public bool IsVedsetningarhlutfallOk { get; set; }

        public bool LenderLendingRulesBroken { get; set; }
        public string LenderLendingRulesBrokenText { get; set; }
        public bool NeedsNekoLoan { get; set; }
        public string LenderNameThagufall { get; set; }
    }
}
