using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.ViewModels
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
