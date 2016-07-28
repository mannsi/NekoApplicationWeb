using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            
        }

        public UserViewModel(ApplicationUser user, bool hasConfirmedEula)
        {
            Id = user.Id;
            Name = user.UserName;
            Email = user.Email;
            FacebookPath = user.FacebookPath;
            TwitterPath = user.TwitterPath;
            LinkedInPath = user.LinkedInPath;
            IsDeletable = user.IsDeletable;
            HasConfirmedEula = hasConfirmedEula;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public bool HasConfirmedEula { get; set; }
        public bool IsDeletable { get; set; }

        [Required(ErrorMessage = "Verður að fylla")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Verður að vera löglegt netfang")]
        public string Email { get; set; }

        [DataType(DataType.Url, ErrorMessage = "Ólögleg slóð")]
        public string FacebookPath { get; set; }

        [DataType(DataType.Url, ErrorMessage = "Ólögleg slóð")]
        public string TwitterPath { get; set; }

        [DataType(DataType.Url, ErrorMessage = "Ólögleg slóð")]
        public string LinkedInPath { get; set; }
    }
}
