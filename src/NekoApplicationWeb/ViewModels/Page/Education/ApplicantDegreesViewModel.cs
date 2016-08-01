using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.ViewModels.Page.Education
{
    public class ApplicantDegreesViewModel
    {
        public ApplicationUser Applicant { get; set; }
        public List<DegreeViewModel> Degrees { get; set; }
    }
}
