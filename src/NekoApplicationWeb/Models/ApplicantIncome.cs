using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NekoApplicationWeb.Models
{
    public enum IncomeType
    {
        Salary,
        Alimony,
        Rent
    }

    public class ApplicantIncome
    {
        public ApplicantIncome()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public ApplicationUser User { get; set; }

        public int MonthlyAmount { get; set; }
        public IncomeType IncomeType { get; set; }
    }
}
