using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ViewModels;

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
        public void UserReadEula([FromBody]ApplicationUser eulaUser)
        {
            var userApplicationConnection =
                _dbContext.ApplicationUserConnections.FirstOrDefault(auc => auc.User.Id == eulaUser.Id);
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
