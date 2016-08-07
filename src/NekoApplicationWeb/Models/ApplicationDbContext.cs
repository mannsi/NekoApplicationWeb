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

        public DbSet<Application> Applications { get; set; }
        public DbSet<Lender> Lenders { get; set; }
        public DbSet<InterestsEntry> InterestsEntries { get; set; }
        public DbSet<ApplicationUserConnection> ApplicationUserConnections { get; set; } 
        public DbSet<ApplicantEducation> ApplicantEducations { get; set; } 
        public DbSet<ApplicantEmployment> ApplicantEmployments { get; set; } 
        public DbSet<Asset> Assets { get; set; } 
        public DbSet<Debt> Debts{ get; set; } 
        public DbSet<ApplicantIncome> ApplicantIncomes { get; set; }
        public DbSet<PropertyDetail> PropertyDetails { get; set; } 
        public DbSet<PropertyValuation> PropertyValuations { get; set; }
        public DbSet<ThjodskraPerson> ThjodskraPersons { get; set; }
        public DbSet<ThjodskraFamilyEntry> ThjodskraFamilyEntries { get; set; }    
        public DbSet<CostOfLivingEntry> CostOfLivingEntries { get; set; }
    }
}
