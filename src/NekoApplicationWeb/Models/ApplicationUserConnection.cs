using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.Models
{
    public class ApplicationUserConnection
    {
        public ApplicationUserConnection()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public Application Application { get; set; }
        public ApplicationUser User { get; set; }
        public bool UserHasAgreedToEula { get; set; }
    }
}
