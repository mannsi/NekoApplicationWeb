using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ViewModels.Page.Education;
using NekoApplicationWeb.ViewModels.Page.Employment;
using NekoApplicationWeb.ViewModels.Page.Finances;
using NekoApplicationWeb.ViewModels.Page.Personal;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NekoApplicationWeb.Controllers.api
{
    [Route("api/finances")]
    [Authorize]
    public class FinancesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public FinancesController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [Route("list")]
        [HttpPost]
        public async Task SaveList([FromBody]FinancesViewModel vm)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);
            var userConnectionForUser = _dbContext.ApplicationUserConnections.Include(connection => connection.Application).First(con => con.User == loggedInUser);
            var application = userConnectionForUser.Application;

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                UpdateIncome(vm.IncomesViewModel);
                UpdateAssets(vm.AssetsViewModel, application);
                UpdateDebts(vm.DebtsViewModel, application);
                transaction.Commit();
            }
        }

        private void UpdateIncome(List<IncomeViewModel> incomesViewModel)
        {
            foreach (var incomeViewModel in incomesViewModel)
            {
                var user = _dbContext.Users.First(u => u.Id == incomeViewModel.Applicant.Id);
                var incomesForUser = _dbContext.ApplicantIncomes.Where(income => income.User == user).ToList();
                _dbContext.RemoveRange(incomesForUser);

                _dbContext.ApplicantIncomes.Add(new ApplicantIncome
                {
                    User = user,
                    IncomeType = IncomeType.Salary,
                    MonthlyAmount = incomeViewModel.SalaryIncome.MonthlyAmount
                });

                foreach (var otherIncome in incomeViewModel.OtherIncomes)
                {
                    _dbContext.ApplicantIncomes.Add(new ApplicantIncome
                    {
                        User = user,
                        IncomeType = otherIncome.IncomeType,
                        MonthlyAmount = otherIncome.MonthlyAmount
                    });
                }
            }

            _dbContext.SaveChanges();
        }

        private void UpdateAssets(List<AssetViewModel> assetsViewModel, Application application)
        {
            var assetsInDb = _dbContext.Assets.Where(ass => ass.Application == application);
            _dbContext.RemoveRange(assetsInDb);

            foreach (var assetViewModel in assetsViewModel)
            {
                _dbContext.Assets.Add(new Asset
                {
                    Application = application,
                    AssetNumber = assetViewModel.AssetNumber,
                    AssetType = assetViewModel.AssetType,
                    AssetWillBeSold = assetViewModel.AssetWillBeSold
                });
            }

            _dbContext.SaveChanges();
        }

        private void UpdateDebts(List<DebtViewModel> debtsViewModel, Application application)
        {
            var debtsInDb = _dbContext.Debts.Where(debt => debt.Application == application);
            _dbContext.RemoveRange(debtsInDb);

            foreach (var debtViewModel in debtsViewModel)
            {
                _dbContext.Debts.Add(new Debt
                {
                    Lender = debtViewModel.Lender,
                    Application = application,
                    DebtType = debtViewModel.DebtType,
                    LoanRemains = debtViewModel.LoanRemains,
                    MonthlyPayment = debtViewModel.MonthlyPayment
                });
            }
            _dbContext.SaveChanges();
        }
    }
}
