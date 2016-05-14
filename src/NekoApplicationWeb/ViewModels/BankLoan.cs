using System.ComponentModel.DataAnnotations;

namespace NekoApplicationWeb.ViewModels
{
    public class BankLoan
    {
        [Required(ErrorMessage = "Verður að fylla")]
        [Display(Name = "Höfuðstóll")]
        [DataType(DataType.Text)]
        public int Principal { get; set; }

        [Required(ErrorMessage = "Verður að fylla")]
        [Display(Name = "Afborganir á mánuði")]
        [DataType(DataType.Text)]
        public int MonthlyPayment { get; set; }
    }
}