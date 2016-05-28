using System.ComponentModel.DataAnnotations;

namespace NekoApplicationWeb.ViewModels.Page
{
    public class PersonalViewModel
    {
        [Required(ErrorMessage = "Verður að fylla")]
        [Display(Name = "Kennitala")]
        [DataType(DataType.Text)]
        public string Ssn { get; set; }

        [Required(ErrorMessage = "Verður að fylla")]
        [DataType(DataType.Text)]
        [Display(Name = "Nafn")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Verður að fylla")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Verður að vera löglegt netfang")]
        [Display(Name = "Netfang")]
        public string Email { get; set; }

        [DataType(DataType.Url, ErrorMessage = "Ólögleg slóð")]
        [Display(Name = "Facebook slóð")]
        public string FacebookPath { get; set; }
    }
}
