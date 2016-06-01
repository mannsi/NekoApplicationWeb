using System.ComponentModel.DataAnnotations;

namespace NekoApplicationWeb.ViewModels.Page.Finances
{
    public enum IncomeType
    {
        Salary,
        Alimony,
        Rent
    }

    public class ApplicantIncome
    {
        private string _incomeTypeString;

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
