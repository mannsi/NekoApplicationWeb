using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.ViewModels
{
    public class EducationViewModel
    {
        public EducationViewModel()
        {
            Degrees = new List<EducationDegree>();
            DegreesSpouse = new List<EducationDegree>();
        }

        public List<EducationDegree> Degrees { get; set; }
        public List<EducationDegree> DegreesSpouse { get; set; }
    }
}
