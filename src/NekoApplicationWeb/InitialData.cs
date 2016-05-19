using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
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
    }
}
