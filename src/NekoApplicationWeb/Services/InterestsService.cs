using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ServiceInterfaces;

namespace NekoApplicationWeb.Services
{
    public class InterestsService : IInterestsService
    {
        public List<InterestsInfo> GetInterestsMatrix(Lender lender)
        {
            // TODO implement different lender interests. Now everything behaves as Landsbankinn
            var interestLines = new List<InterestsInfo>
            {
                new InterestsInfo {LoanType = LoanType.Regular, Indexed = false, LoanToValueStartPercentage = 0, LoanToValueEndPercentage = 70, InterestsForm = InterestsForm.Variable,
                    InterestPercentage = 7.25, LoanPaymentType = LoanPaymentType.Annuitet, LoanTimeYearsMin = 1, LoanTimeYearsMax = 40},
                new InterestsInfo {LoanType = LoanType.Regular, Indexed = false, LoanToValueStartPercentage = 0, LoanToValueEndPercentage = 70, InterestsForm = InterestsForm.Fixed,
                    InterestPercentage = 7.30, LoanPaymentType = LoanPaymentType.Annuitet, LoanTimeYearsMin = 1, LoanTimeYearsMax = 40, FixedInterestsYears = 3},
                new InterestsInfo {LoanType = LoanType.Regular, Indexed = false, LoanToValueStartPercentage = 0, LoanToValueEndPercentage = 70, InterestsForm = InterestsForm.Fixed,
                    InterestPercentage = 7.45, LoanPaymentType = LoanPaymentType.Annuitet, LoanTimeYearsMin = 1, LoanTimeYearsMax = 40, FixedInterestsYears = 5},
                new InterestsInfo {LoanType = LoanType.Regular, Indexed = true, LoanToValueStartPercentage = 0, LoanToValueEndPercentage = 70, InterestsForm = InterestsForm.Variable,
                    InterestPercentage = 3.65, LoanPaymentType = LoanPaymentType.Annuitet, LoanTimeYearsMin = 5, LoanTimeYearsMax = 40},
                new InterestsInfo {LoanType = LoanType.Regular, Indexed = true, LoanToValueStartPercentage = 0, LoanToValueEndPercentage = 70, InterestsForm = InterestsForm.Fixed,
                    InterestPercentage = 3.85, LoanPaymentType = LoanPaymentType.Annuitet, LoanTimeYearsMin = 5, LoanTimeYearsMax = 40, FixedInterestsYears = 5},
                new InterestsInfo {LoanType = LoanType.Additional, Indexed = false, LoanToValueStartPercentage = 70, LoanToValueEndPercentage = 85, InterestsForm = InterestsForm.Variable,
                    InterestPercentage = 8.25, LoanPaymentType = LoanPaymentType.EvenPayments, LoanTimeYearsMin = 1, LoanTimeYearsMax = 15},
                new InterestsInfo {LoanType = LoanType.Additional, Indexed = false, LoanToValueStartPercentage = 70, LoanToValueEndPercentage = 85, InterestsForm = InterestsForm.Fixed,
                    InterestPercentage = 8.30, LoanPaymentType = LoanPaymentType.EvenPayments, LoanTimeYearsMin = 1, LoanTimeYearsMax = 15, FixedInterestsYears = 3},
                new InterestsInfo {LoanType = LoanType.Additional, Indexed = false, LoanToValueStartPercentage = 70, LoanToValueEndPercentage = 85, InterestsForm = InterestsForm.Fixed,
                    InterestPercentage = 8.45, LoanPaymentType = LoanPaymentType.EvenPayments, LoanTimeYearsMin = 1, LoanTimeYearsMax = 15, FixedInterestsYears = 5},
                new InterestsInfo {LoanType = LoanType.Additional, Indexed = true, LoanToValueStartPercentage = 70, LoanToValueEndPercentage = 85, InterestsForm = InterestsForm.Variable,
                    InterestPercentage = 4.65, LoanPaymentType = LoanPaymentType.EvenPayments, LoanTimeYearsMin = 5, LoanTimeYearsMax = 15},
            };

            var nekoInterests = GetNekoInterestInfo();
            interestLines.Add(nekoInterests);

            return interestLines;
        }

        private InterestsInfo GetNekoInterestInfo()
        {
            return new InterestsInfo
            {
                Indexed = true,
                InterestPercentage = 8,
                InterestsForm = InterestsForm.Fixed,
                FixedInterestsYears = 15,
                LoanTimeYearsMax = 15,
                LoanTimeYearsMin = 15,
                LoanType = LoanType.Neko,
                LoanPaymentType = LoanPaymentType.Neko
            };
        }

    }
}
