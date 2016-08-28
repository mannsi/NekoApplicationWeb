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

    }
}
