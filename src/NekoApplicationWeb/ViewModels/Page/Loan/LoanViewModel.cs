using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.ViewModels.Page.Loan
{
    public enum Lenders
    {
        Landsbanki,
        ArionBanki,
        IslandsBanki,
        FrjalsiLifeyrissjodurinn
    }

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

        public string Lender { get; set; }

        public List<BankLoanViewModel> BankLoans { get; set; }
    }
}
