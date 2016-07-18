using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb
{
    public class InitialData
    {
        private const string DemoUserSsn = "1111119999";
        private const string DemoUserEmail = "Hans ";
        private const string DemoUserPassword = "123456";

        public static async Task CreateDemoUser(IServiceProvider applicationServices)
        {
            // Create super user if he does not exist and add super user role to him
            var userManager = (UserManager<ApplicationUser>)applicationServices.GetService(typeof(UserManager<ApplicationUser>));
            var demoUser = await userManager.FindByIdAsync(DemoUserSsn);

            if (demoUser == null)
            {
                demoUser = new ApplicationUser
                {
                    UserName =  DemoUserSsn,
                    Email = DemoUserEmail,
                    EmailConfirmed = true,
                    UserDisplayName = "Hans Jón Sigurðsson"
                };

                await userManager.CreateAsync(demoUser);
                await userManager.AddPasswordAsync(demoUser, DemoUserPassword);
            }
        }

        public static void CreateLenders(IServiceProvider applicationServices)
        {
            // TODO move these bank ids to some other place where I can refernecs also when I am processing data
            var dbContext = (ApplicationDbContext) applicationServices.GetService(typeof (ApplicationDbContext));
            bool lenderAdded = false;
            if (!dbContext.Lenders.Any(lender => lender.Id == Shared.Constants.ArionId))
            {
                dbContext.Lenders.Add(new Lender {Id = Shared.Constants.ArionId, Name = "Arion banki", LoanPaymentServiceFee = 635});
                lenderAdded = true;
            }

            if (!dbContext.Lenders.Any(lender => lender.Id == Shared.Constants.LandsbankinnId))
            {
                dbContext.Lenders.Add(new Lender { Id = Shared.Constants.LandsbankinnId, Name = "Landsbankinn", LoanPaymentServiceFee = 635 });
                lenderAdded = true;
            }

            if (!dbContext.Lenders.Any(lender => lender.Id == Shared.Constants.IslandsbankiId))
            {
                dbContext.Lenders.Add(new Lender { Id = Shared.Constants.IslandsbankiId, Name = "Íslandsbank", LoanPaymentServiceFee = 595 });
                lenderAdded = true;
            }
            
            if (!dbContext.Lenders.Any(lender => lender.Id == Shared.Constants.FrjalsiLifeyrissjodurinnId))
            {
                dbContext.Lenders.Add(new Lender { Id = Shared.Constants.FrjalsiLifeyrissjodurinnId, Name = "Frjálsi lífeyrissjóðurinn", LoanPaymentServiceFee = 595 });
                lenderAdded = true;
            }
            
            if (!dbContext.Lenders.Any(lender => lender.Id == Shared.Constants.AlmenniLifeyrissjodurinnId))
            {
                dbContext.Lenders.Add(new Lender { Id = Shared.Constants.AlmenniLifeyrissjodurinnId, Name = "Almenni lífeyrissjóðurinn", LoanPaymentServiceFee = 595 });
                lenderAdded = true;
            }

            if (lenderAdded)
            {
                dbContext.SaveChanges();
            }
        }
    }
}
