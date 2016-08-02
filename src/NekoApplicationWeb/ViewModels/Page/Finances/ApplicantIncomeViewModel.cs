using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.ViewModels.Page.Finances
{
    public class ApplicantIncomeViewModel
    {
        private string _incomeTypeString;

        public string Id { get; set; }
        public int MonthlyAmount { get; set; }
        public IncomeType IncomeType { get; set; }

        public string IncomeTypeString
        {
            get
            {
                if (!string.IsNullOrEmpty(_incomeTypeString))
                {
                    return _incomeTypeString;
                }

                switch (IncomeType)
                {
                    case IncomeType.Alimony:
                        return "Meðlag";
                    case IncomeType.Rent:
                        return "Leigutekjur";
                    case IncomeType.Salary:
                        return "Laun";
                }
                return "";
            }
            set { _incomeTypeString = value; }
        }
    }
}
