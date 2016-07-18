using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NekoApplicationWeb.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Lender>()
                .Property(lender => lender.LoanPaymentServiceFee)
                .HasDefaultValue(130);

            base.OnModelCreating(builder);
        }

        public DbSet<Lender> Lenders { get; set; }
    }
}
