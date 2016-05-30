using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.ViewModels.Page.Education
{
    public class ApplicantDegreesViewModel
    {
        public string ApplicantName { get; set; }
        public List<DegreeViewModel> Degrees { get; set; }
    }
}
