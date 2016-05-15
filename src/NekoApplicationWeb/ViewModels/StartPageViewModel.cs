using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.ViewModels
{
    public class StartPageViewModel
    {
        [Required(ErrorMessage="Verður að fylla")]
        [Display(Name = "Kennitala")]
        [DataType(DataType.Text)]
        public string Ssn { get; set; }

        [Required(ErrorMessage = "Verður að fylla")]
        [Display(Name = "Lykilorð")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
