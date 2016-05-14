using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NekoApplicationWeb.ViewModels
{
    public class LoanViewModel
    {
        [Required(ErrorMessage = "Verður að fylla")]
        [Display(Name = "Kaupverð")]
        [DataType(DataType.Text)]
        public int BuyingPrice { get; set; }

        [Required(ErrorMessage = "Verður að fylla")]
        [Display(Name = "Fastanúmer eignar")]
        [DataType(DataType.Text)]
        public string PropertyNumber { get; set; }

        [Required(ErrorMessage = "Verður að fylla")]
        [Display(Name = "Fjárhæð Neko láns")]
        [DataType(DataType.Text)]
        public int NekoLoanAmount { get; set; }

        public List<BankLoan> BankLoans { get; set; }
    }

}