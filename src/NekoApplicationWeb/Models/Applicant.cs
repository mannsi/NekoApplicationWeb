using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.Models
{
    public class Applicant
    {
        public string Id { get; set; }
        public DateTime Added { get; set; }
        public string Ssn { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string FacebookPath { get; set; }
    }
}
