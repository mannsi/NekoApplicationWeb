using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace NekoApplicationWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string UserDisplayName { get; set; }
    }
}
