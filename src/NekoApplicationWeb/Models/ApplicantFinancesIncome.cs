using System.ComponentModel.DataAnnotations.Schema;

namespace NekoApplicationWeb.Models
{
    public enum IncomeType
    {
        Salary,
        Alimony,
        Rent
    }

    public class ApplicantFinancesIncome
    {
        private string _incomeTypeString;

        public string Id { get; set; }
        public ApplicationUser User { get; set; }

        public int MonthlyAmount { get; set; }
        public IncomeType IncomeType { get; set; }

        [NotMapped]
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
