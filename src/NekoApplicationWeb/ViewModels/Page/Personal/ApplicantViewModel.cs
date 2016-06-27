using System.ComponentModel.DataAnnotations;

namespace NekoApplicationWeb.ViewModels.Page.Personal
{
    public class ApplicantViewModel
    {
        [Required(ErrorMessage = "Verður að fylla")]
        [DataType(DataType.Text)]
        public string Ssn { get; set; }

        [Required(ErrorMessage = "Verður að fylla")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Verður að fylla")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Verður að vera löglegt netfang")]
        public string Email { get; set; }

        [DataType(DataType.Url, ErrorMessage = "Ólögleg slóð")]
        public string FacebookPath { get; set; }

        [DataType(DataType.Url, ErrorMessage = "Ólögleg slóð")]
        public string TwitterPath { get; set; }

        [DataType(DataType.Url, ErrorMessage = "Ólögleg slóð")]
        public string LinkedInPath { get; set; }
    }
}
