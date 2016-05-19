using System.ComponentModel.DataAnnotations;

namespace NekoApplicationWeb.ViewModels.Account
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
