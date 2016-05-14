using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.ViewModels
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
