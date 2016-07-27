using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.ViewModels.Page.Finances
{
    public class ApplicantFinancesViewModel
    {
        public ApplicantFinancesViewModel()
        {
            OtherIcome = new List<ApplicantFinancesIncome>();
            Assets = new List<ApplicantFinancesAsset>();
            Debts = new List<ApplicantFinancesDebt>();
        }

        public string ApplicantSsn { get; set; }
        public string ApplicantName { get; set; }
        public ApplicantFinancesIncome MonthlyPay { get; set; }
        public List<ApplicantFinancesIncome> OtherIcome { get; set; }
        public List<ApplicantFinancesAsset> Assets { get; set; }
        public List<ApplicantFinancesDebt> Debts { get; set; }
    }
}
