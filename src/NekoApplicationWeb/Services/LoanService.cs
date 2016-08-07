using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ServiceInterfaces;
using NekoApplicationWeb.ViewModels.Page.Loan;
using MoreLinq;

namespace NekoApplicationWeb.Services
{
    public class LoanService : ILoanService
    {
        public List<BankLoanViewModel> GetDefaultLoansForLender(
            Lender lender, 
            int buyingPrice, 
            int ownCapital,
            List<InterestsEntry> interestLinesForLender,
            int realEstateValuation, 
            int newFireInsuranceValuation,
            int plotAssessmentValue)
        {
            var bankLoans = new BankLoans();

            switch (lender.Id)
            {
                case Shared.Constants.LandsbankinnId:
                    bankLoans = LandsbankinnLoans(realEstateValuation, newFireInsuranceValuation, plotAssessmentValue,
                        buyingPrice, ownCapital, interestLinesForLender, lender.LoanPaymentServiceFee);
                    break;
                case Shared.Constants.ArionId:
                    bankLoans = ArionBankiLoans(realEstateValuation, newFireInsuranceValuation, plotAssessmentValue,
                        buyingPrice, ownCapital, interestLinesForLender);
                    break;
                case Shared.Constants.IslandsbankiId:
                    // TODO
                    bankLoans = null;
                    break;
                case Shared.Constants.FrjalsiLifeyrissjodurinnId:
                    // TODO
                    bankLoans = null;
                    break;
                case Shared.Constants.AlmenniLifeyrissjodurinnId:
                    // TODO
                    bankLoans = null;
                    break;
            }

            if (bankLoans == null) return null;

            var nekoInteresInfo = interestLinesForLender.FirstOrDefault(interest => interest.LoanType == LoanType.Neko);
            if (nekoInteresInfo == null) return null;

            var nekoLoan = GetNekoLoan(bankLoans.TotalPrincipal, buyingPrice, ownCapital, nekoInteresInfo);
            bankLoans.Add(nekoLoan);

            return bankLoans.Loans;

        }

        private BankLoanViewModel GetNekoLoan(int totalBankLoansPrincipal, int buyingPrice, int ownCapital, InterestsEntry nekoInteresEntry)
        {
            int loanPrincipal = buyingPrice - totalBankLoansPrincipal - ownCapital;
            var interest = nekoInteresEntry.InterestPercentage/100;
            int monthlyPayment = (int)(loanPrincipal * (interest / 12) / (1 - Math.Pow(1 + interest / 12, -12 * 10)));

            var nekoLoan = new BankLoanViewModel
            {
                InterestEntry = nekoInteresEntry,
                LoanDurationYears = 15,
                LoanDurationMaxYears = 15,
                LoanDurationMinYears = 15,
                Principal = loanPrincipal,
                MonthlyPayment = monthlyPayment,
            };

            return nekoLoan;
        }

        private BankLoans LandsbankinnLoans(int realEstateValuation, int newFireInsuranceValuation,
            int plotAssessmentValue, int buyingPrice, int ownCapital, List<InterestsEntry> interestMatrix, int serviceFee)
        {
            // Landsbankinn will loan up to 85% of buying price, but never more than newFireInsuranceValuation + plotAssessmentValue

            var loans = new BankLoans();
            var totalLoanNeeded = buyingPrice - ownCapital;
            var maxLoanGivenByLandsbankinn = newFireInsuranceValuation + plotAssessmentValue;
            var totalLandsbankinnLoanAmount = Math.Min(totalLoanNeeded, maxLoanGivenByLandsbankinn);

            // If borrower requires below 85% he does not need Neko loan
            if (1.0*totalLoanNeeded/buyingPrice <= 0.85) return null;

            #region LoanA

            var loanAInterestLines = interestMatrix.Where(matrix => !matrix.Indexed &&
                                                                    matrix.LoanToValueStartPercentage == 0 && 50 <= matrix.LoanToValueEndPercentage &&
                                                                    matrix.LoanPaymentType == LoanPaymentType.Annuitet &&
                                                                    matrix.LoanTimeYearsMax == 40).ToList();

            // No interests line fits the business rule
            if (!loanAInterestLines.Any()) return null;

            var loanAInterestLine = loanAInterestLines.MinBy(matrix => matrix.InterestPercentage);

            var loanAMaxPrincipal =(int) Math.Max(Math.Round(buyingPrice * 0.5) - ownCapital, 0);
            var loanAPrincipal = Math.Min(loanAMaxPrincipal, totalLandsbankinnLoanAmount);
            var loanAInterests = loanAInterestLine.InterestPercentage  / 100;
            var loanAMonthlyPayments = (int)Math.Round((loanAInterests / 12) / (1 - Math.Pow(1 + loanAInterests / 12, -(12*40))) * loanAPrincipal) + serviceFee;

            var loanA = new BankLoanViewModel
            {
                Principal = loanAPrincipal,
                LoanDurationYears = 40,
                LoanDurationMaxYears = 40,
                LoanDurationMinYears = 15,
                InterestEntry = loanAInterestLine,
                MonthlyPayment = loanAMonthlyPayments
            };
            loans.Add(loanA);

            // Got all the loans that landsbankinn will give
            if (loans.TotalPrincipal == totalLandsbankinnLoanAmount) return loans;
            #endregion

            #region LoanB
            var loanBInterestLines = interestMatrix.Where(matrix => matrix.Indexed &&
                                                                    matrix.LoanToValueStartPercentage <= 50 && 70 <= matrix.LoanToValueEndPercentage &&
                                                                    matrix.LoanPaymentType == LoanPaymentType.Annuitet &&
                                                                    matrix.LoanTimeYearsMax == 40).ToList();
            // No interests line fits the business rule
            if (!loanBInterestLines.Any()) return null;

            var loanBInterestLine = loanBInterestLines.MinBy(matrix => matrix.InterestPercentage);

            var loanBMaxPrincipal = (int) Math.Max(Math.Round(buyingPrice * 0.7 - (loans.TotalPrincipal + ownCapital)), 0);
            var loanBPrincipal = Math.Min(loanBMaxPrincipal, totalLandsbankinnLoanAmount - loans.TotalPrincipal);
            var loanBInterests = loanBInterestLine.InterestPercentage / 100;
            var loanBMonthlyPayments = (int)Math.Round((loanBInterests / 12) / (1 - Math.Pow(1 + loanBInterests / 12, -(12 * 40))) * loanBPrincipal) + serviceFee;

            var loanB = new BankLoanViewModel
            {
                Principal = loanBPrincipal,
                LoanDurationYears = 40,
                LoanDurationMaxYears = 40,
                LoanDurationMinYears = 15,
                InterestEntry = loanBInterestLine,
                MonthlyPayment = loanBMonthlyPayments
            };
            loans.Add(loanB);

            // Got all the loans that landsbankinn will give
            if (loans.TotalPrincipal == totalLandsbankinnLoanAmount) return loans;
            #endregion

            #region LoanC
            var loanCInterestLines = interestMatrix.Where(matrix => matrix.Indexed &&
                                                                    matrix.LoanToValueStartPercentage <= 70 && 85 <= matrix.LoanToValueEndPercentage &&
                                                                    matrix.LoanPaymentType == LoanPaymentType.EvenPayments &&
                                                                    matrix.LoanTimeYearsMax == 15).ToList();
            // No interests line fits the business rule
            if (!loanCInterestLines.Any()) return null;

            var loanCInterestLine = loanCInterestLines.MinBy(matrix => matrix.InterestPercentage);

            var loanCMaxPrincipal = (int)Math.Max(Math.Round(buyingPrice * 0.85 - (loans.TotalPrincipal + ownCapital)),0);

            var loanCPrincipal = Math.Min(loanCMaxPrincipal, totalLandsbankinnLoanAmount - loans.TotalPrincipal);
            var loanCInterests = loanCInterestLine.InterestPercentage / 100;
            var loanCMonthlyPayments = (int)Math.Round((loanCInterests/12)*loanCPrincipal + (loanCPrincipal/(12*15.0))) + serviceFee;


            var loanC = new BankLoanViewModel
            {
                Principal = loanCPrincipal,
                LoanDurationYears = 15,
                LoanDurationMaxYears = 15, 
                LoanDurationMinYears = 15,
                InterestEntry = loanCInterestLine,
                MonthlyPayment = loanCMonthlyPayments
            };
            loans.Add(loanC);

            #endregion

            return loans;
        }

        private BankLoans ArionBankiLoans(int realEstateValuation, int newFireInsuranceValuation,
            int plotAssessmentValue, int buyingPrice, int ownCapital, List<InterestsEntry> interestsLines)
        {
            return null;
        }
    }
}
