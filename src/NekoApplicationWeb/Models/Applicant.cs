using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.Models
{
    public class Applicant
    {
        public string Legend { get; set; } // F.x. Umsækjandi 2
        public string Ssn { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string FacebookPath { get; set; }
    }
}
