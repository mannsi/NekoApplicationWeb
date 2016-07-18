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
        private readonly ApplicationDbContext _dbContext;
        private readonly IInterestsService _interestsService;

        public LoanService(
            ApplicationDbContext dbContext
            , IInterestsService interestsService)
        {
            _dbContext = dbContext;
            _interestsService = interestsService;
        }


        public List<BankLoanViewModel> GetDefaultLoansForLender(string lenderId, string propertyNumber, int buyingPrice, int ownCapital)
        {
            var selectedLender = _dbContext.Lenders.FirstOrDefault(lender => lender.Id == lenderId);
            if (selectedLender == null) return null;

            // TODO fetch data based on property number
            int realEstateValuation = 25000000; // TODO Fictional numer to try out calculations
            int newFireInsuranceValuation = 23000000; // TODO Fictional numer to try out calculations
            int plotAssessmentValue = 3500000; // TODO Fictional numer to try out calculations

            var interestLinesForLender = _interestsService.GetInterestsMatrix(lenderId);
            var bankLoans = new BankLoans();

            switch (lenderId)
            {
                case Shared.Constants.LandsbankinnId:
                    bankLoans = LandsbankinnLoans(realEstateValuation, newFireInsuranceValuation, plotAssessmentValue,
                        buyingPrice, ownCapital, interestLinesForLender, selectedLender.LoanPaymentServiceFee);
                    break;
                case Shared.Constants.ArionId:
                    bankLoans = ArionBankiLoans(realEstateValuation, newFireInsuranceValuation, plotAssessmentValue,
                        buyingPrice, ownCapital, interestLinesForLender);
                    break;
                case Shared.Constants.IslandsbankiId:
                    // TODO
                    break;
                case Shared.Constants.FrjalsiLifeyrissjodurinnId:
                    // TODO
                    break;
                case Shared.Constants.AlmenniLifeyrissjodurinnId:
                    // TODO
                    break;
            }

            var nekoLoan = GetNekoLoan(bankLoans.TotalPrincipal, buyingPrice, ownCapital);
            bankLoans.Add(nekoLoan);

            return bankLoans.Loans;

        }

        private BankLoanViewModel GetNekoLoan(int totalBankLoansPrincipal, int buyingPrice, int ownCapital)
        {
            var interestInfo = new InterestsInfo
            {
                Indexed = true,
                InterestPercentage = 8,
                InterestsForm = InterestsForm.Fixed,
                LoanTimeYearsMax = 15,
                LoanTimeYearsMin = 15,
                LoanType = LoanType.Neko,
                LoanPaymentType = LoanPaymentType.Neko
            };

            int loanPrincipal = buyingPrice - totalBankLoansPrincipal - ownCapital;
            int monthlyPayment = (int) (interestInfo.InterestPercentage*loanPrincipal) / 12;

            var nekoLoan = new BankLoanViewModel
            {
                InterestInfo = interestInfo,
                LoanDurationYears = 15,
                LoanDurationMaxYears = 15,
                LoanDurationMinYears = 15,
                Principal = loanPrincipal,
                MonthlyPayment = monthlyPayment
            };

            return nekoLoan;
        }

        private BankLoans LandsbankinnLoans(int realEstateValuation, int newFireInsuranceValuation,
            int plotAssessmentValue, int buyingPrice, int ownCapital, List<InterestsInfo> interestMatrix, int serviceFee)
        {
            // Landsbankinn will loan up to 85% of buying price, but never more than newFireInsuranceValuation + plotAssessmentValue

            var loans = new BankLoans();
            var totalLoanNeeded = buyingPrice - ownCapital;
            var maxLoanGivenByLandsbankinn = (int)0.85*(newFireInsuranceValuation + plotAssessmentValue);
            var totalLandsbankinnLoanAmount = Math.Min(totalLoanNeeded, maxLoanGivenByLandsbankinn);

            #region LoanA
            var loanAInterestLine = interestMatrix.Where(matrix => !matrix.Indexed &&
                                                                    matrix.LoanToValueStartPercentage == 0 && matrix.LoanToValueEndPercentage <= 50 &&
                                                                    matrix.LoanPaymentType == LoanPaymentType.Annuitet &&
                                                                    matrix.LoanTimeYearsMax == 40).MinBy(matrix => matrix.InterestPercentage);
            if (loanAInterestLine == null)
            {
                // No interests line fits the business rule
                return null;
            }

            var loanAMaxPrincipal = (int)(buyingPrice * 0.5) - ownCapital;
            var loanAPrincipal = Math.Min(loanAMaxPrincipal, totalLandsbankinnLoanAmount);
            var loanAInterestsPercentage = loanAInterestLine.InterestPercentage;
            var loanAMonthlyPayments = (int)((loanAInterestsPercentage / 12) / (1 - Math.Pow(1 + loanAInterestsPercentage / 12, -(12*40))))* loanAPrincipal + serviceFee;

            var loanA = new BankLoanViewModel
            {
                Principal = loanAPrincipal,
                LoanDurationYears = 40,
                LoanDurationMaxYears = 40,
                LoanDurationMinYears = 15,
                InterestInfo = loanAInterestLine,
                MonthlyPayment = loanAMonthlyPayments
            };
            loans.Add(loanA);

            // Got all the loans that landsbankinn will give
            if (loans.TotalPrincipal == totalLandsbankinnLoanAmount) return loans;
            #endregion

            #region LoanB
            var loanBInterestLine = interestMatrix.Where(matrix => matrix.Indexed &&
                                                                    50 <= matrix.LoanToValueStartPercentage && matrix.LoanToValueEndPercentage <= 70 &&
                                                                    matrix.LoanPaymentType == LoanPaymentType.Annuitet &&
                                                                    matrix.LoanTimeYearsMax == 40).MinBy(matrix => matrix.InterestPercentage);
            // No interests line fits the business rule
            if (loanBInterestLine == null) return null;

            var loanBMaxPrincipal = (int) (buyingPrice*0.7) - loanA.Principal;
            var loanBPrincipal = Math.Min(loanBMaxPrincipal, totalLandsbankinnLoanAmount - loans.TotalPrincipal);
            var loanBInterestsPercentage = loanBInterestLine.InterestPercentage;
            var loanBMonthlyPayments = (int)((loanBInterestsPercentage / 12) / (1 - Math.Pow(1 + loanBInterestsPercentage / 12, -(12 * 40)))) * loanBPrincipal + serviceFee;

            var loanB = new BankLoanViewModel
            {
                Principal = loanBPrincipal,
                LoanDurationYears = 40,
                LoanDurationMaxYears = 40,
                LoanDurationMinYears = 15,
                InterestInfo = loanBInterestLine,
                MonthlyPayment = loanBMonthlyPayments
            };
            loans.Add(loanB);

            // Got all the loans that landsbankinn will give
            if (loans.TotalPrincipal == totalLandsbankinnLoanAmount) return loans;
            #endregion

            #region LoanC
            var loanCInterestLine = interestMatrix.Where(matrix => matrix.Indexed &&
                                                                    70 <= matrix.LoanToValueStartPercentage && matrix.LoanToValueEndPercentage <= 85 &&
                                                                    matrix.LoanPaymentType == LoanPaymentType.EvenPayments &&
                                                                    matrix.LoanTimeYearsMax == 15).MinBy(matrix => matrix.InterestPercentage);
            // No interests line fits the business rule
            if (loanCInterestLine == null) return null;

            var loanCMaxPrincipal = (int)(buyingPrice * 0.85) - loanA.Principal - loanB.Principal;

            var loanCPrincipal = Math.Min(loanCMaxPrincipal, totalLandsbankinnLoanAmount - loans.TotalPrincipal);
            var loanCInterestsPercentage = loanCInterestLine.InterestPercentage;
            var loanCMonthlyPayments = (int)(loanCInterestsPercentage/12)*loanCPrincipal + (loanCPrincipal/(12*15)) + serviceFee;


            var loanC = new BankLoanViewModel
            {
                Principal = loanCPrincipal,
                LoanDurationYears = 15,
                LoanDurationMaxYears = 15, 
                LoanDurationMinYears = 15,
                InterestInfo = loanCInterestLine,
                MonthlyPayment = loanCMonthlyPayments
            };
            loans.Add(loanC);

            #endregion

            return loans;
        }

        private BankLoans ArionBankiLoans(int realEstateValuation, int newFireInsuranceValuation,
            int plotAssessmentValue, int buyingPrice, int ownCapital, List<InterestsInfo> interestsLines)
        {
            return null;
        }
    }
}
