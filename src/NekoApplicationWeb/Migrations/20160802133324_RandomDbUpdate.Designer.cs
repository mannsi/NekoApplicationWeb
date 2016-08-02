using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160802133324_RandomDbUpdate")]
    partial class RandomDbUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.ApplicantEducation", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("Degree");

                    b.Property<DateTime>("FinishingDate");

                    b.Property<string>("School");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ApplicationEducations");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.ApplicantEmployment", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("Company");

                    b.Property<DateTime>("StartingTime");

                    b.Property<string>("Title");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ApplicantEmployments");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.ApplicantIncome", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("IncomeType");

                    b.Property<int>("MonthlyAmount");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ApplicantIncomes");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.Application", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("CreatedByUserId");

                    b.Property<DateTime>("TimeCreated");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FacebookPath");

                    b.Property<bool>("IsDeletable");

                    b.Property<string>("LinkedInPath");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("TwitterPath");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.ApplicationUserConnection", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ApplicationId");

                    b.Property<bool>("UserHasAgreedToEula");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("UserId");

                    b.ToTable("ApplicationUserConnections");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.Asset", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ApplicationId");

                    b.Property<string>("AssetNumber");

                    b.Property<int>("AssetType");

                    b.Property<bool>("AssetWillBeSold");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.Debt", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ApplicationId");

                    b.Property<int>("DebtType");

                    b.Property<string>("Lender");

                    b.Property<int>("LoanRemains");

                    b.Property<int>("MonthlyPayment");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("Debts");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.ExternalLoanDetails", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ApplicationId");

                    b.Property<string>("LenderId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("LenderId");

                    b.ToTable("LoanDetails");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.InterestsEntry", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("FixedInterestsYears");

                    b.Property<bool>("Indexed");

                    b.Property<double>("InterestPercentage");

                    b.Property<int>("InterestsForm");

                    b.Property<string>("LenderId");

                    b.Property<int>("LoanPaymentType");

                    b.Property<int>("LoanTimeYearsMax");

                    b.Property<int>("LoanTimeYearsMin");

                    b.Property<int>("LoanToValueEndPercentage");

                    b.Property<int>("LoanToValueStartPercentage");

                    b.Property<int>("LoanType");

                    b.HasKey("Id");

                    b.HasIndex("LenderId");

                    b.ToTable("InterestsEntries");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.Lender", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("LoanPaymentServiceFee")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(130);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Lenders");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.PropertyDetail", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ApplicationId");

                    b.Property<int>("BuyingPrice");

                    b.Property<int>("OwnCapital");

                    b.Property<string>("PropertyNumber");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("PropertyDetails");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.PropertyValuation", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ApplicationId");

                    b.Property<DateTime>("TimeOfData");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("PropertyValuations");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.ThjodskraFamilyEntry", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("FamilyNumber");

                    b.Property<int>("GenderCode");

                    b.Property<string>("Name");

                    b.Property<string>("Ssn");

                    b.Property<DateTime>("TimeOfData");

                    b.HasKey("Id");

                    b.ToTable("ThjodskraFamilyEntries");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.ThjodskraPerson", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("FamilyNumber");

                    b.Property<int>("GenderCode");

                    b.Property<string>("Home");

                    b.Property<int>("MaritalStatus");

                    b.Property<string>("Name");

                    b.Property<string>("PlaceOfResidence");

                    b.Property<string>("PostCode");

                    b.Property<string>("SpouseSsn");

                    b.Property<DateTime>("TimeOfData");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ThjodskraPersons");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("NekoApplicationWeb.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("NekoApplicationWeb.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NekoApplicationWeb.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.ApplicantEducation", b =>
                {
                    b.HasOne("NekoApplicationWeb.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.ApplicantEmployment", b =>
                {
                    b.HasOne("NekoApplicationWeb.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.ApplicantIncome", b =>
                {
                    b.HasOne("NekoApplicationWeb.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.Application", b =>
                {
                    b.HasOne("NekoApplicationWeb.Models.ApplicationUser", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.ApplicationUserConnection", b =>
                {
                    b.HasOne("NekoApplicationWeb.Models.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId");

                    b.HasOne("NekoApplicationWeb.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.Asset", b =>
                {
                    b.HasOne("NekoApplicationWeb.Models.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.Debt", b =>
                {
                    b.HasOne("NekoApplicationWeb.Models.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.ExternalLoanDetails", b =>
                {
                    b.HasOne("NekoApplicationWeb.Models.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId");

                    b.HasOne("NekoApplicationWeb.Models.Lender", "Lender")
                        .WithMany()
                        .HasForeignKey("LenderId");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.InterestsEntry", b =>
                {
                    b.HasOne("NekoApplicationWeb.Models.Lender", "Lender")
                        .WithMany()
                        .HasForeignKey("LenderId");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.PropertyDetail", b =>
                {
                    b.HasOne("NekoApplicationWeb.Models.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.PropertyValuation", b =>
                {
                    b.HasOne("NekoApplicationWeb.Models.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId");
                });

            modelBuilder.Entity("NekoApplicationWeb.Models.ThjodskraPerson", b =>
                {
                    b.HasOne("NekoApplicationWeb.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
        }
    }
}
