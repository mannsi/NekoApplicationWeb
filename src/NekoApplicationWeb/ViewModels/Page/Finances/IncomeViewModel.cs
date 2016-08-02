using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.ViewModels.Page.Finances
{
    public class IncomeViewModel
    {
        public IncomeViewModel()
        {
            OtherIncomes = new List<ApplicantIncomeViewModel>();
        }

        public ApplicationUser Applicant { get; set; }
        public ApplicantIncomeViewModel SalaryIncome { get; set; }
        public List<ApplicantIncomeViewModel> OtherIncomes { get; set; }
    }
}
