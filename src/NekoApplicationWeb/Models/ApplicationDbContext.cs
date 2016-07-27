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
        public DbSet<InterestsEntry> InterestsEntries { get; set; }
        public DbSet<ApplicationUserConnection> ApplicationUserConnections { get; set; } 
        public DbSet<ApplicantEducation> ApplicationEducations { get; set; } 
        public DbSet<ApplicantEmployment> ApplicantEmployments { get; set; } 
        public DbSet<ApplicantFinancesAsset> ApplicantFinancesAssets { get; set; } 
        public DbSet<ApplicantFinancesDebt> ApplicantFinancesDebts{ get; set; } 
        public DbSet<ApplicantFinancesIncome> ApplicantFinancesIncomes { get; set; }
        public DbSet<PropertyDetail> PropertyDetails { get; set; } 
        public DbSet<PropertyValuation> PropertyValuations { get; set; }
        public DbSet<ExternalLoanDetails> LoanDetails { get; set; }
        public DbSet<ThjodskraPerson> ThjodskraPersons { get; set; }
        public DbSet<ThjodskraFamilyEntry> ThjodskraFamilyEntries { get; set; }    
    }
}
