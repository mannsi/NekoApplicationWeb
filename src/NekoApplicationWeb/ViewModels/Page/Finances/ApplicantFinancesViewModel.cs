using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NekoApplicationWeb.ViewModels.Page.Finances
{
    public class ApplicantFinancesViewModel
    {
        public ApplicantFinancesViewModel()
        {
            OtherIcome = new List<ApplicantIncome>();
            Assets = new List<ApplicantAssets>();
            Debts = new List<ApplicantDebt>();
        }

        public string ApplicantName { get; set; }
        public ApplicantIncome MonthlyPay { get; set; }
        public List<ApplicantIncome> OtherIcome { get; set; }
        public List<ApplicantAssets> Assets { get; set; }
        public List<ApplicantDebt> Debts { get; set; }
    }
}
