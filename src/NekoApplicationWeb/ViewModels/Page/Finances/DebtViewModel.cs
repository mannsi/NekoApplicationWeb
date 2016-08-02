using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.ViewModels.Page.Finances
{
    public class DebtViewModel
    {
        private string _debtTypeString;

        public string Id { get; set; }
        public DebtType DebtType { get; set; }
        public string Lender { get; set; }
        public int LoanRemains { get; set; }
        public int MonthlyPayment { get; set; }

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
