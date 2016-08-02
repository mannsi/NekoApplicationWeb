using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.ViewModels.Page.Finances
{
    public class FinancesViewModel
    {
        public FinancesViewModel()
        {
            IncomesViewModel = new List<IncomeViewModel>();
            AssetsViewModel = new List<AssetViewModel>();
            DebtsViewModel = new List<DebtViewModel>();
        }

        public List<IncomeViewModel> IncomesViewModel { get; set; }
        public List<AssetViewModel> AssetsViewModel { get; set; }
        public List<DebtViewModel> DebtsViewModel { get; set; }
    }
}
