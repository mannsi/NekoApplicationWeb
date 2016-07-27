using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.Models
{
    public class ApplicantEducation
    {
        public string Id { get; set; }
        public ApplicationUser User { get; set; }
        public string School { get; set; }
        public string Degree { get; set; }
        public DateTime FinishingDate { get; set; }
    }
}
