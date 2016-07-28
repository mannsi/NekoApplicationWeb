using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace NekoApplicationWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsDeletable { get; set; }
        public string FacebookPath { get; set; }
        public string TwitterPath { get; set; }
        public string LinkedInPath { get; set; }
    }
}
