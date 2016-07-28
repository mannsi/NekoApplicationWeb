using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.ViewModels
{
    public class UserViewModel : ApplicationUser
    {
        public bool HasConfirmedEula { get; set; }
    }
}
