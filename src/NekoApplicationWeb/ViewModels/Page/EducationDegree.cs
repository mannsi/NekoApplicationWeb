using System;
using System.ComponentModel.DataAnnotations;

namespace NekoApplicationWeb.ViewModels.Page
{
    public class EducationDegree
    {
        [Required(ErrorMessage = "Verður að fylla")]
        [Display(Name = "Skóli")]
        [DataType(DataType.Text)]
        public string School { get; set; }

        [Required(ErrorMessage = "Verður að fylla")]
        [DataType(DataType.Text)]
        [Display(Name = "Gráða")]
        public string Degree { get; set; }

        [Required(ErrorMessage = "Verður að fylla")]
        [DataType(DataType.Date)]
        [Display(Name = "Námslok")]
        public DateTime DateFinished { get; set; }

    }
}
