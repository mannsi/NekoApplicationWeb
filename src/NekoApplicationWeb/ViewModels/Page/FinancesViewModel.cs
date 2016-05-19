using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NekoApplicationWeb.ViewModels.Page
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
