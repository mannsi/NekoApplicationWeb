using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb
{
    public class InitialData
    {
        private const string DemoUserSsn = "1111119999";
        private const string DemoUserEmail = "Hans ";
        private const string DemoUserPassword = "123456";

        public static async Task CreateDemoUser(IServiceProvider applicationServices)
        {
            // Create super user if he does not exist and add super user role to him
            var userManager = (UserManager<ApplicationUser>)applicationServices.GetService(typeof(UserManager<ApplicationUser>));
            var demoUser = await userManager.FindByIdAsync(DemoUserSsn);

            if (demoUser == null)
            {
                demoUser = new ApplicationUser
                {
                    UserName =  DemoUserSsn,
                    Email = DemoUserEmail,
                    EmailConfirmed = true,
                    //UserDisplayName = "Hans Jón Sigurðsson"
                };

                await userManager.CreateAsync(demoUser);
                await userManager.AddPasswordAsync(demoUser, DemoUserPassword);
            }
        }

        public static void CreateLenders(IServiceProvider applicationServices)
        {
            var dbContext = (ApplicationDbContext) applicationServices.GetService(typeof (ApplicationDbContext));
            bool lenderAdded = false;
            if (!dbContext.Lenders.Any(lender => lender.Id == Shared.Constants.ArionId))
            {
                dbContext.Lenders.Add(new Lender {Id = Shared.Constants.ArionId, Name = "Arion banki", LoanPaymentServiceFee = 635});
                lenderAdded = true;
            }

            if (!dbContext.Lenders.Any(lender => lender.Id == Shared.Constants.LandsbankinnId))
            {
                dbContext.Lenders.Add(new Lender { Id = Shared.Constants.LandsbankinnId, Name = "Landsbankinn", LoanPaymentServiceFee = 635 });
                lenderAdded = true;
            }

            if (!dbContext.Lenders.Any(lender => lender.Id == Shared.Constants.IslandsbankiId))
            {
                dbContext.Lenders.Add(new Lender { Id = Shared.Constants.IslandsbankiId, Name = "Íslandsbanki", LoanPaymentServiceFee = 595 });
                lenderAdded = true;
            }
            
            if (!dbContext.Lenders.Any(lender => lender.Id == Shared.Constants.FrjalsiLifeyrissjodurinnId))
            {
                dbContext.Lenders.Add(new Lender { Id = Shared.Constants.FrjalsiLifeyrissjodurinnId, Name = "Frjálsi lífeyrissjóðurinn", LoanPaymentServiceFee = 595 });
                lenderAdded = true;
            }
            
            if (!dbContext.Lenders.Any(lender => lender.Id == Shared.Constants.AlmenniLifeyrissjodurinnId))
            {
                dbContext.Lenders.Add(new Lender { Id = Shared.Constants.AlmenniLifeyrissjodurinnId, Name = "Almenni lífeyrissjóðurinn", LoanPaymentServiceFee = 595 });
                lenderAdded = true;
            }

            if (!dbContext.Lenders.Any(lender => lender.Id == Shared.Constants.NekoLenderId))
            {
                dbContext.Lenders.Add(new Lender { Id = Shared.Constants.NekoLenderId, Name = "Neko", LoanPaymentServiceFee = 0 });
                lenderAdded = true;
            }

            if (lenderAdded)
            {
                dbContext.SaveChanges();
            }
        }

        public static void CreateCostOfLivingEntries(IServiceProvider applicationServices)
        {
            var dbContext = (ApplicationDbContext) applicationServices.GetService(typeof (ApplicationDbContext));

            if (dbContext.CostOfLivingEntries.Any()) return;

            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(1, 0, 0, 91766, 10900));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(1, 0, 1, 160825, 21800));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(1, 1, 0, 158758, 21800));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(1, 0, 2, 216479, 32700));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(1, 4, 1, 221308, 32700));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(1, 2, 0, 213940, 32700));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(1, 0, 3, 259259, 43600));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(1, 1, 2, 270453, 43600));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(1, 2, 1, 269981, 43600));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(1, 3, 0, 259960, 43600));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(1, 0, 4, 283734, 54500));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(1, 1, 3, 301490, 54500));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(1, 2, 2, 307383, 54500));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(1, 3, 1, 304259, 54500));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(1, 4, 0, 284436, 54500));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(2, 0, 0, 157840, 21800));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(2, 0, 1, 226899, 32700));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(2, 1, 0, 233536, 32700));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(2, 0, 2, 282555, 43600));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(2, 1, 1, 296088, 43600));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(2, 2, 0, 292254, 43600));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(2, 0, 3, 325334, 54500));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(2, 1, 2, 345233, 54500));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(2, 2, 1, 348295, 54500));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(2, 3, 0, 338200, 54500));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(2, 0, 4, 349808, 65400));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(2, 1, 3, 376268, 65400));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(2, 2, 2, 385695, 65400));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(2, 3, 1, 382496, 65400));
            dbContext.CostOfLivingEntries.Add(new CostOfLivingEntry(2, 4, 0, 362673, 65400));

            dbContext.SaveChanges();
        }

        public static void CreateInterestInfo(IServiceProvider applicationServices)
        {
            var dbContext = (ApplicationDbContext)applicationServices.GetService(typeof(ApplicationDbContext));

            var neko = dbContext.Lenders.FirstOrDefault(lender => lender.Id == Shared.Constants.NekoLenderId);
            if (neko == null) return;
            if (!dbContext.InterestsEntries.Any(ie => ie.Lender == neko))
            {
                CreateNekoInterestInfo(neko, dbContext);
            }

            var landsbankinn = dbContext.Lenders.FirstOrDefault(lender => lender.Id == Shared.Constants.LandsbankinnId);
            if (landsbankinn == null) return;

            if (!dbContext.InterestsEntries.Any(ie => ie.Lender == landsbankinn))
            {
                CreateLandsbankinnInterestInfo(landsbankinn, dbContext);
            }
        }

        private static void CreateLandsbankinnInterestInfo(Lender landsbankinn, ApplicationDbContext dbContext)
        {
            // Here are the landsbankinn interest info entries
            var interestsEntries = new List<InterestsEntry>
            {
                new InterestsEntry {LoanType = LoanType.Regular, Indexed = false, LoanToValueStartPercentage = 0, LoanToValueEndPercentage = 70, InterestsForm = InterestsForm.Variable,
                    InterestPercentage = 7.25, LoanPaymentType = LoanPaymentType.Annuitet, LoanTimeYearsMin = 1, LoanTimeYearsMax = 40},
                new InterestsEntry {LoanType = LoanType.Regular, Indexed = false, LoanToValueStartPercentage = 0, LoanToValueEndPercentage = 70, InterestsForm = InterestsForm.Fixed,
                    InterestPercentage = 7.30, LoanPaymentType = LoanPaymentType.Annuitet, LoanTimeYearsMin = 1, LoanTimeYearsMax = 40, FixedInterestsYears = 3},
                new InterestsEntry {LoanType = LoanType.Regular, Indexed = false, LoanToValueStartPercentage = 0, LoanToValueEndPercentage = 70, InterestsForm = InterestsForm.Fixed,
                    InterestPercentage = 7.45, LoanPaymentType = LoanPaymentType.Annuitet, LoanTimeYearsMin = 1, LoanTimeYearsMax = 40, FixedInterestsYears = 5},
                new InterestsEntry {LoanType = LoanType.Regular, Indexed = true, LoanToValueStartPercentage = 0, LoanToValueEndPercentage = 70, InterestsForm = InterestsForm.Variable,
                    InterestPercentage = 3.65, LoanPaymentType = LoanPaymentType.Annuitet, LoanTimeYearsMin = 5, LoanTimeYearsMax = 40},
                new InterestsEntry {LoanType = LoanType.Regular, Indexed = true, LoanToValueStartPercentage = 0, LoanToValueEndPercentage = 70, InterestsForm = InterestsForm.Fixed,
                    InterestPercentage = 3.85, LoanPaymentType = LoanPaymentType.Annuitet, LoanTimeYearsMin = 5, LoanTimeYearsMax = 40, FixedInterestsYears = 5},
                new InterestsEntry {LoanType = LoanType.Additional, Indexed = false, LoanToValueStartPercentage = 70, LoanToValueEndPercentage = 85, InterestsForm = InterestsForm.Variable,
                    InterestPercentage = 8.25, LoanPaymentType = LoanPaymentType.EvenPayments, LoanTimeYearsMin = 1, LoanTimeYearsMax = 15},
                new InterestsEntry {LoanType = LoanType.Additional, Indexed = false, LoanToValueStartPercentage = 70, LoanToValueEndPercentage = 85, InterestsForm = InterestsForm.Fixed,
                    InterestPercentage = 8.30, LoanPaymentType = LoanPaymentType.EvenPayments, LoanTimeYearsMin = 1, LoanTimeYearsMax = 15, FixedInterestsYears = 3},
                new InterestsEntry {LoanType = LoanType.Additional, Indexed = false, LoanToValueStartPercentage = 70, LoanToValueEndPercentage = 85, InterestsForm = InterestsForm.Fixed,
                    InterestPercentage = 8.45, LoanPaymentType = LoanPaymentType.EvenPayments, LoanTimeYearsMin = 1, LoanTimeYearsMax = 15, FixedInterestsYears = 5},
                new InterestsEntry {LoanType = LoanType.Additional, Indexed = true, LoanToValueStartPercentage = 70, LoanToValueEndPercentage = 85, InterestsForm = InterestsForm.Variable,
                    InterestPercentage = 4.65, LoanPaymentType = LoanPaymentType.EvenPayments, LoanTimeYearsMin = 5, LoanTimeYearsMax = 15},
            };

            foreach (var interestsEntry in interestsEntries)
            {
                interestsEntry.Lender = landsbankinn;
                dbContext.InterestsEntries.Add(interestsEntry);
            }

            dbContext.SaveChanges();
        }

        private static void CreateNekoInterestInfo(Lender neko, ApplicationDbContext dbContext)
        {
            var interestsEntry = new InterestsEntry
            {
                Indexed = true,
                InterestPercentage = 8,
                InterestsForm = InterestsForm.Fixed,
                FixedInterestsYears = 15,
                LoanTimeYearsMax = 15,
                LoanTimeYearsMin = 15,
                LoanType = LoanType.Neko,
                LoanPaymentType = LoanPaymentType.Neko,
                Lender = neko
            };

            dbContext.InterestsEntries.Add(interestsEntry);
            dbContext.SaveChanges();
        }
    }
}
