using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace NekoApplicationWeb.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public Application ActiveApplication(ClaimsPrincipal loggedInUserPrincipal)
        {
            var loggedInUserId = _userManager.GetUserId(loggedInUserPrincipal);
            var userConnection = _dbContext.ApplicationUserConnections.Include(con => con.Application).ThenInclude(application => application.Lender).FirstOrDefault(con => con.User.Id == loggedInUserId);

            return userConnection?.Application;
        }

        public void ApplyForNekoLoan(ClaimsPrincipal loggedInUserPrincipal)
        {
            var application = ActiveApplication(loggedInUserPrincipal);

            if (application.HasApplied) return;

            // TODO verify application just like is done in summary page
            // TODO Get lanshaefismat
            // TODO if lanshaefismat sucks, send email to user informing 
            // TODO Get greidslumat (if needed)
            // TODO if greislumat sucks, send email to user informing 
            // TODO we got here, applicants appear to be solid. Send them email asking for documents 

            application.HasApplied = true;
            _dbContext.Update(application);
            _dbContext.SaveChanges();
        }
    }
}
