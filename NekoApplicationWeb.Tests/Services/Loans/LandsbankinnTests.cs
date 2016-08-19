using System.Collections.Generic;
using System.Linq;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.Services;
using NekoApplicationWeb.ViewModels.Page.Loan;
using Xunit;

namespace NekoApplicationWeb.Tests.Loans
{
    public class LandsbankinnTests
    {
        private readonly LoanService _loanService;
        private readonly int _serviceFee = 1000;

        public LandsbankinnTests()
        {
            _loanService = new LoanService();
        }

        private Lender GetLandsBankinn()
        {
            return new Lender() {Id = NekoApplicationWeb.Shared.Constants.LandsbankinnId, LoanPaymentServiceFee = _serviceFee };
        }

        private List<InterestsEntry> GetLandsbankinnInterests()
        {
            var interestLines = new List<InterestsEntry>
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

            var nekoInterests =  new InterestsEntry
            {
                Indexed = true,
                InterestPercentage = 8,
                InterestsForm = InterestsForm.Fixed,
                LoanTimeYearsMax = 15,
                LoanTimeYearsMin = 15,
                LoanType = LoanType.Neko,
                LoanPaymentType = LoanPaymentType.Neko
            };

            interestLines.Add(nekoInterests);

            return interestLines;
        }

        [Fact]
        public void NormalLoanNumbersTest()
        {
            var lender = GetLandsBankinn();
            var interests = GetLandsbankinnInterests();

            var buyingPrice = 25000000;
            var ownCapital = 500000;
            int realEstateValuation = 25000000;
            int newFireInsuranceValuation = 23000000;
            int plotAssessmentValue = 3500000;

            var loans = _loanService.GetDefaultLoansForLender(lender, buyingPrice, ownCapital, interests, realEstateValuation,
                newFireInsuranceValuation, plotAssessmentValue);

            Assert.Equal(4, loans.Count);

            // These numbers are calculated from the Landsbankinn LoanTest calculations google sheet
            var bankLoan1 = loans.First(loan => loan.Principal == 12000000);
            Assert.Equal(77761, bankLoan1.MonthlyPayment);

            var bankLoan2 = loans.First(loan => loan.Principal == 5000000);
            Assert.Equal(20822, bankLoan2.MonthlyPayment);

            var bankLoan3 = loans.First(loan => loan.Principal == 3750000 && loan.InterestEntry.LoanType != LoanType.Neko);
            Assert.Equal(36365, bankLoan3.MonthlyPayment);

            var nekoLoan = loans.First(loan => loan.Principal == 3750000 && loan.InterestEntry.LoanType == LoanType.Neko);
            Assert.Equal(25000, nekoLoan.MonthlyPayment);
        }

        [Fact]
        public void PlotAndFireEvalLimit_ReturnLowerLoan()
        {
            // Here we limit Landsbankinn with low newFireInsuranceValuation and plotAssessmentValue
            var lender = GetLandsBankinn();
            var interests = GetLandsbankinnInterests();

            var buyingPrice = 25000000;
            var ownCapital = 500000;
            int realEstateValuation = 25000000;
            int newFireInsuranceValuation = 10000000;
            int plotAssessmentValue = 3500000;

            var loans = _loanService.GetDefaultLoansForLender(lender, buyingPrice, ownCapital, interests, realEstateValuation,
                newFireInsuranceValuation, plotAssessmentValue);

            Assert.Equal(3, loans.Count);

            // These numbers are calculated from the Landsbankinn LoanTest calculations google sheet
            // Loans should not exceed newFireInsuranceValuation + plotAssessmentValue
            var bankLoan1 = loans.First(loan => loan.Principal == 12000000);
            Assert.Equal(77761, bankLoan1.MonthlyPayment);

            var bankLoan2 = loans.First(loan => loan.Principal == 1500000);
            Assert.Equal(6947, bankLoan2.MonthlyPayment);

            var nekoLoan = loans.First(loan => loan.Principal == 11000000);
            Assert.Equal(73333, nekoLoan.MonthlyPayment);
        }

        [Fact]
        public void MissingInterestsLines_ReturnNull_Test()
        {
            var lender = GetLandsBankinn();
            var interests = GetLandsbankinnInterests();
            interests.Clear();

            var loans = _loanService.GetDefaultLoansForLender(lender, 0, 0, interests, 0, 0, 0);
            Assert.Equal(0, loans.Count);
        }

        [Fact]
        public void BorrowerOnlyNeedsBelow85Percent_DoesNotNeedNekoLoan_Test()
        {
            // Borrower has 20% capital and does not need Neko loan
            var lender = GetLandsBankinn();
            List<InterestsEntry> interests = null;

            var buyingPrice = 25000000;
            var ownCapital = 5000000;
            int realEstateValuation = 0;
            int newFireInsuranceValuation = 0;
            int plotAssessmentValue = 0;

            var loans = _loanService.GetDefaultLoansForLender(lender, buyingPrice, ownCapital, interests, realEstateValuation,
                newFireInsuranceValuation, plotAssessmentValue);
            Assert.Equal(0, loans.Count);
        }


        [Fact]
        public void BorrowerNeeds95Percent_Return4Loans_Test()
        {
            var lender = GetLandsBankinn();
            var interests = GetLandsbankinnInterests();

            var buyingPrice = 25000000;
            var ownCapital = 1250000;
            int realEstateValuation = 25000000;
            int newFireInsuranceValuation = 23000000;
            int plotAssessmentValue = 3500000;

            var loans = _loanService.GetDefaultLoansForLender(lender, buyingPrice, ownCapital, interests, realEstateValuation,
                newFireInsuranceValuation, plotAssessmentValue);

            Assert.Equal(4, loans.Count);

            // These numbers are calculated from the Landsbankinn LoanTest calculations google sheet
            var bankLoan1 = loans.First(loan => loan.Principal == 11250000);
            Assert.Equal(72963, bankLoan1.MonthlyPayment);

            var bankLoan2 = loans.First(loan => loan.Principal == 5000000);
            Assert.Equal(20822, bankLoan2.MonthlyPayment);

            var bankLoan3 = loans.First(loan => loan.Principal == 3750000 && loan.InterestEntry.LoanType != LoanType.Neko);
            Assert.Equal(36365, bankLoan3.MonthlyPayment);

            var nekoLoan = loans.First(loan => loan.Principal == 3750000 && loan.InterestEntry.LoanType == LoanType.Neko);
            Assert.Equal(25000, nekoLoan.MonthlyPayment);
        }
    }
}
