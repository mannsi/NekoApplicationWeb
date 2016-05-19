using System.Collections.Generic;

namespace NekoApplicationWeb.ViewModels.Page
{
    public class EmploymentViewModel
    {
        public EmploymentViewModel()
        {
            Employements = new List<Employment>();
            EmployementsSpouse = new List<Employment>();
        }

        public List<Employment> Employements { get; set; }
        public List<Employment> EmployementsSpouse { get; set; }
    }
}
