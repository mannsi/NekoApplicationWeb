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
        public InterestsInfo InterestInfo { get; set; }
        public int MonthlyPayment { get; set; }

        public string InterestsFormString
        {
            get
            {
                switch (InterestInfo.InterestsForm)
                {
                    case InterestsForm.Fixed:
                        return $"Fastir-{InterestInfo.FixedInterestsYears} ár";
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
                switch (InterestInfo.LoanPaymentType)
                {
                    case LoanPaymentType.Annuitet:
                        return "Jafnar greiðslur";
                    case LoanPaymentType.EvenPayments:
                        return "Jafnar afborganir";
                    default:
                        return "";
                }
            }
        }

        public bool IsNekoLoan => InterestInfo.LoanPaymentType == LoanPaymentType.Neko;
    }
}


