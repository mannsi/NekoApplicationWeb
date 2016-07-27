using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ViewModels.Account;
using NekoApplicationWeb.ViewModels.Page.Personal;
using NekoApplicationWeb.ViewModels.Page.Start;

namespace NekoApplicationWeb.Controllers.api
{
    [Route("api/application")]
    [Authorize]
    public class ApplicationController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ApplicationController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("readEula")]
        [HttpPost]
        public void UserReadEula(StartPageViewModel vm)
        {
            var userApplicationConnection =
                _dbContext.ApplicationUserConnections.FirstOrDefault(auc => auc.User.Id == vm.UserId);
            if (userApplicationConnection == null)
            {
                return;
            }

            userApplicationConnection.UserHasAgreedToEula = true;
            _dbContext.Update(userApplicationConnection);
            _dbContext.SaveChanges();
        }

        
    }
}
