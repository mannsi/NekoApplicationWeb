using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace NekoApplicationWeb.Models
{
    public enum DebtType
    {
        StudentLoan,
        Overdraft,
        CarLoan,
        PropertyLoan,
        Other
    }

    public class ApplicantFinancesDebt
    {
        private string _debtTypeString;
       
        public DebtType DebtType { get; set; }
        public string Lender { get; set; }
        public int LoanRemains { get; set; }
        public int MonthlyPayment { get; set; }

        [NotMapped]
        public string DebtTypeString
        {
            get
            {
                if (!string.IsNullOrEmpty(_debtTypeString))
                {
                    return _debtTypeString;
                }

                switch (DebtType)
                {
                    case DebtType.StudentLoan:
                        return "Námslán";
                    case DebtType.Overdraft:
                        return "Yfirdráttur";
                    case DebtType.CarLoan:
                        return "Bílalán/Rekstrarleiga";
                    case DebtType.PropertyLoan:
                        return "Húsnæðislán";
                    case DebtType.Other:
                        return "Annað";
                }
                return "";
            }
            set { _debtTypeString = value; }
        }

    }
}
