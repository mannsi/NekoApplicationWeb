using System.ComponentModel.DataAnnotations;

namespace NekoApplicationWeb.ViewModels.Page
{
    public class OtherLoan
    {
        [Required(ErrorMessage = "Verður að fylla")]
        [Display(Name = "Eftirstöðvar")]
        [DataType(DataType.Text)]
        public int Remains { get; set; }

        [Required(ErrorMessage = "Verður að fylla")]
        [Display(Name = "Afborgun á mánuði")]
        [DataType(DataType.Text)]
        public int MonthlyPayment { get; set; }
    }
}
