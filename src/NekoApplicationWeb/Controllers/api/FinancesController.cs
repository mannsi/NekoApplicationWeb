﻿using System;
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
using System.ServiceModel.Syndication;
using Microsoft.EntityFrameworkCore;
using NekoApplicationWeb.ServiceInterfaces;

namespace NekoApplicationWeb.Controllers.api
{
    [Route("api/finances")]
    [Authorize]
    public class FinancesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompletionService _completionService;

        public FinancesController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, ICompletionService completionService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _completionService = completionService;
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
                _dbContext.Update(application);
                UpdateIncome(vm.IncomesViewModel, application);
                UpdateAssets(vm.AssetsViewModel, application);
                UpdateDebts(vm.DebtsViewModel, application);
                transaction.Commit();
            }

            // Update the completion status of the application
            application.FinancesPageCompleted = _completionService.FinancesCompleted(User);
            _dbContext.Update(application);
            _dbContext.SaveChanges();
        }

        private void UpdateIncome(List<IncomeViewModel> incomesViewModel, Application application)
        {
            int totalIncome = 0;
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
                totalIncome += incomeViewModel.SalaryIncome.MonthlyAmount;

                foreach (var otherIncome in incomeViewModel.OtherIncomes)
                {
                    _dbContext.ApplicantIncomes.Add(new ApplicantIncome
                    {
                        User = user,
                        IncomeType = otherIncome.IncomeType,
                        MonthlyAmount = otherIncome.MonthlyAmount
                    });
                    totalIncome += otherIncome.MonthlyAmount;
                }
            }

            application.TotalMonthlyIncomeForAllApplicant = totalIncome;

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
            int totalDebt = 0;

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

                totalDebt += debtViewModel.LoanRemains;
            }

            application.TotalDebtAmountForAllApplicants = totalDebt;

            _dbContext.SaveChanges();
        }
    }
}
