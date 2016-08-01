using System;
using System.ComponentModel.DataAnnotations;

namespace NekoApplicationWeb.ViewModels.Page.Education
{
    public class DegreeViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Verður að fylla")]
        [DataType(DataType.Text)]
        public string School { get; set; }

        [Required(ErrorMessage = "Verður að fylla")]
        [DataType(DataType.Text)]
        public string Degree { get; set; }

        [Required(ErrorMessage = "Verður að fylla")]
        [DataType(DataType.Date)]
        public DateTime DateFinished { get; set; }

    }
}
