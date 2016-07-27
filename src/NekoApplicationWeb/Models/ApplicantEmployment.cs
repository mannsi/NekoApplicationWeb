using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.Models
{
    public class ApplicantEmployment
    {
        public string Id { get; set; }
        public ApplicationUser User { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public DateTime StartingTime { get; set; }
    }
}
