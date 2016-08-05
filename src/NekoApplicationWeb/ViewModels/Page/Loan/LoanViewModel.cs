using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.ViewModels.Page.Loan
{
    public class LoanViewModel
    {
        public LoanViewModel()
        {
            BankLoans = new List<BankLoanViewModel>();
        }

        [Required]
        public int BuyingPrice { get; set; }
        [Required]
        public string PropertyNumber { get; set; }
        [Required]
        public int OwnCapital { get; set; }
    
        public Lender Lender { get; set; }

        public List<BankLoanViewModel> BankLoans { get; set; }

    }
}
