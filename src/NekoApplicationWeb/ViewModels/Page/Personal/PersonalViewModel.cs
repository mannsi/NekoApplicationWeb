using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.ViewModels.Page.Personal
{
    public class PersonalViewModel
    {
        public bool ShowEula { get; set; }
        public ApplicationUser EulaUser { get; set; }
        public List<UserViewModel> Applicants { get; set; }
    }
}
