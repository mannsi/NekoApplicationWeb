using System.Collections.Generic;

namespace NekoApplicationWeb.ViewModels.Page
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
