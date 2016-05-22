
using System.ComponentModel.DataAnnotations;

namespace NekoApplicationWeb.ViewModels.Account
{
    public class CreateApplicationViewModel
    {
        [Required(ErrorMessage="Verður að fylla")]
        [Display(Name = "Kennitala")]
        [DataType(DataType.Text)]
        public string Ssn { get; set; }

        [Required(ErrorMessage="Verður að fylla")]
        [Display(Name = "Netfang")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Ólöglegt netfang")]
        public string Email { get; set; }

        [Required(ErrorMessage="Verður að fylla")]
        [Display(Name="Lykilorð")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage="Verður að fylla")]
        [Display(Name="Staðfesta lykilorð")]
        [DataType(DataType.Password)]
        [Compare(otherProperty:"Password", ErrorMessage = "Lykilorð og staðfest lykilorð eru ekki eins")]
        public string ConfirmPassword { get; set; }
    }
}
