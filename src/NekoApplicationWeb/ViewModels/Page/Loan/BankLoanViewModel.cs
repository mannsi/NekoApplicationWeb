using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.ViewModels.Page.Loan
{
    public class BankLoanViewModel
    {
        public int Principal { get; set; }
        public int LoanDurationYears { get; set; }
        public int LoanDurationMaxYears { get; set; }
        public int LoanDurationMinYears { get; set; }
        public InterestsEntry InterestEntry { get; set; }
        public int MonthlyPayment { get; set; }
        public int MonthlyPaymentIn5Years { get; set; }

        public string InterestsFormString
        {
            get
            {
                switch (InterestEntry.InterestsForm)
                {
                    case InterestsForm.Fixed:
                        return $"Fastir-{InterestEntry.FixedInterestsYears} ár";
                    case InterestsForm.Variable:
                        return "Breytilegir";
                    default:
                        return "";
                }
            }
        }

        public string PaymentTypeString
        {
            get
            {
                switch (InterestEntry.LoanPaymentType)
                {
                    case LoanPaymentType.Annuitet:
                        return "Jafnar greiðslur";
                    case LoanPaymentType.EvenPayments:
                        return "Jafnar afborganir";
                    case LoanPaymentType.Neko:
                        return "Jafnar afborganir";
                    default:
                        return "";
                }
            }
        }

        public bool IsNekoLoan => InterestEntry.LoanPaymentType == LoanPaymentType.Neko;
    }
}


