using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.ViewModels
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
