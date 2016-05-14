using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.ViewModels
{
    public class Employment
    {
        [Required(ErrorMessage = "Verður að fylla")]
        [Display(Name = "Titill")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Verður að fylla")]
        [DataType(DataType.Text)]
        [Display(Name = "Fyirtæki")]
        public string CompanyName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Frá")]
        public DateTime From { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Til")]
        public DateTime To { get; set; }

    }
}
