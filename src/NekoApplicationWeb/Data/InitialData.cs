using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Identity;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.Data
{
    public class InitialData
    {
        public static void CreateLenders(IServiceProvider applicationServices)
        {
            var dbContext = (ApplicationDbContext) applicationServices.GetService(typeof (ApplicationDbContext));
            bool lenderAdded = false;
            if (!dbContext.Lenders.Any(lender => lender.Id == Shared.Constants.ArionId))
            {
                dbContext.Lenders.Add(new Lender
                {
                    Id = Shared.Constants.ArionId, Name = "Arion banki", NameThagufall = "Arion banka", LoanPaymentServiceFee = 635,
                    MaxDebtServiceToIncome = 30
                });
                lenderAdded = true;
            }

            if (!dbContext.Lenders.Any(lender => lender.Id == Shared.Constants.LandsbankinnId))
            {
                dbContext.Lenders.Add(new Lender
                {
                    Id = Shared.Constants.LandsbankinnId, Name = "Landsbankinn", NameThagufall = "Landsbankanum", LoanPaymentServiceFee = 635,
                    MaxDebtServiceToIncome = 30
                });
                lenderAdded = true;
            }

            if (!dbContext.Lenders.Any(lender => lender.Id == Shared.Constants.IslandsbankiId))
            {
                dbContext.Lenders.Add(new Lender
                {
                    Id = Shared.Constants.IslandsbankiId, Name = "Íslandsbanki", NameThagufall = "Íslandsbanka", LoanPaymentServiceFee = 595,
                    MaxDebtServiceToIncome = 35
                });
                lenderAdded = true;
            }
            
            if (!dbContext.Lenders.Any(lender => lender.Id == Shared.Constants.FrjalsiLifeyrissjodurinnId))
            {
                dbContext.Lenders.Add(new Lender
                {
                    Id = Shared.Constants.FrjalsiLifeyrissjodurinnId,
                    Name = "Frjálsi lífeyrissjóðurinn",
                    NameThagufall = "Frjálsa lífeyrissjóðnum",
                    LoanPaymentServiceFee = 595,
                    MaxDebtServiceToIncome = 30
                });
                lenderAdded = true;
            }
            
            if (!dbContext.Lenders.Any(lender => lender.Id == Shared.Constants.AlmenniLifeyrissjodurinnId))
            {
                dbContext.Lenders.Add(new Lender
                {
                    Id = Shared.Constants.AlmenniLifeyrissjodurinnId,
                    Name = "Almenni lífeyrissjóðurinn",
                    NameThagufall = "Almenna lífeyrissjóðnum",
                    LoanPaymentServiceFee = 595,
                    MaxDebtServiceToIncome = 30
                });
                lenderAdded = true;
            }

            if (!dbContext.Lenders.Any(lender => lender.Id == Shared.Constants.NekoLenderId))
            {
                dbContext.Lenders.Add(new Lender
                {
                    Id = Shared.Constants.NekoLenderId, Name = "Neko", LoanPaymentServiceFee = 0, NameThagufall = "Neko", MaxDebtServiceToIncome = 30
                });
                lenderAdded = true;
            }

            if (lenderAdded)
            {
                dbContext.SaveChanges();
            }
        }

        public static void ImportPropertyValuationData(IServiceProvider applicationServices)
        {
            var dbContext = (ApplicationDbContext)applicationServices.GetService(typeof(ApplicationDbContext));
            if (dbContext.PropertyValuations.Any()) return;

            var propertyValuationList = ReadPropertyValuationDataCsv();
            dbContext.PropertyValuations.AddRange(propertyValuationList);
            dbContext.SaveChanges();
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

        private static List<PropertyValuation> ReadPropertyValuationDataCsv()
        {
            var propertyValuationList = new List<PropertyValuation>();

            var streamReader = File.OpenText(@"Data\ibudalisti.csv");
            var csv = new CsvReader(streamReader);
            csv.Configuration.Delimiter = ";";

            //int brunabotaMatCounter = 0;
            //int lodamatCounter = 0;
            //int fastMat2016Counter = 0;
            //int fastMat2017Counter = 0;
            while (csv.Read())
            {
                var propertyNumber = csv.GetField<string>(0);
                var area = csv.GetField<string>(1);
                var newFireInsuranceValuationString = csv.GetField<string>(2);
                var plotAssessmentValueString = csv.GetField<string>(3);
                var realEstateValuation2016String = csv.GetField<string>(4);
                var realEstateValuation2017String = csv.GetField<string>(5);

                var propertyValuation = new PropertyValuation()
                {
                    PropertyNumber = propertyNumber,
                };

                int newFireInsuranceValuation;
                int plotAssessmentValue;
                int realEstateValuation2016;
                int realEstateValuation2017;

                bool brunabotaMatOk = Int32.TryParse(newFireInsuranceValuationString, out newFireInsuranceValuation);
                bool lodamatOk = Int32.TryParse(plotAssessmentValueString, out plotAssessmentValue);
                bool fastMat2016Ok = Int32.TryParse(realEstateValuation2016String, out realEstateValuation2016);
                bool fastMat2017Ok = Int32.TryParse(realEstateValuation2017String, out realEstateValuation2017);

                propertyValuation.NewFireInsuranceValuation = newFireInsuranceValuation;
                propertyValuation.PlotAssessmentValue = plotAssessmentValue;
                propertyValuation.RealEstateValuation2016 = realEstateValuation2016;
                propertyValuation.RealEstateValuation2017 = realEstateValuation2017;

                //if (!success) throw new Exception($"Error converting property valuation line for property number {propertyNumber}");
                //if (!brunabotaMatOk) brunabotaMatCounter++;
                //if (!lodamatOk) lodamatCounter++; 
                //if (!fastMat2016Ok) fastMat2016Counter++; 
                //if (!fastMat2017Ok) fastMat2017Counter++; 


                propertyValuationList.Add(propertyValuation);
            }
            return propertyValuationList;
        }
    }
}
