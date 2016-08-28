using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.Models
{
    public class Application
    {
        public Application()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public DateTime TimeCreated { get; set; }
        public ApplicationUser CreatedByUser { get; set; }
        public Lender Lender { get; set; }

        public int TotalMonthlyIncomeForAllApplicant { get; set; }
        public int TotalDebtAmountForAllApplicants { get; set; }
        public int TotalAssetAmountForAllApplicants { get; set; }

        public bool PersonalPageCompleted { get; set; }
        public bool EmploymentPageCompleted { get; set; }
        public bool EducationPageCompleted { get; set; }
        public bool FinancesPageCompleted { get; set; }
        public bool LoanPageCompleted { get; set; }
    }
}
