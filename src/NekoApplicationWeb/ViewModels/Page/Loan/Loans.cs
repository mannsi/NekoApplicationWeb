using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NekoApplicationWeb.ViewModels.Page.Loan;

namespace NekoApplicationWeb.Models
{
    public class BankLoans
    {
        private readonly List<BankLoanViewModel> _bankLoans;

        public BankLoans()
        {
            _bankLoans = new List<BankLoanViewModel>();
        }

        public void Add(BankLoanViewModel bankLoan)
        {
            _bankLoans.Add(bankLoan);
        }

        public int TotalPrincipal
        {
            get
            {
                return _bankLoans.Select(loan => loan.Principal).Sum();
            }
        }

        public List<BankLoanViewModel> Loans
        {
            get
            {
                return _bankLoans;
            }
        }
    }
}
