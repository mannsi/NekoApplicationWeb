using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.ViewModels
{
    public class FinancesViewModel
    {
        public FinancesViewModel()
        {
            OtherLoans = new List<OtherLoan>();
        }

        [Required(ErrorMessage = "Verður að fylla")]
        [Display(Name = "Tekjur á mánuði fyrir skatt")]
        [DataType(DataType.Currency)]
        public int MonthlyPayPreTax { get; set; }

        [Display(Name = "Tekjur á mánuði fyrir skatt")]
        [DataType(DataType.Currency)]
        public int MonthlyPayPreTaxSpouse { get; set; }

        public List<OtherLoan> OtherLoans { get; set; }
    }
}
