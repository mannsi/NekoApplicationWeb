using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ServiceInterfaces;
using NekoApplicationWeb.ViewModels;

namespace NekoApplicationWeb.Controllers.api
{
    [Route("api/application")]
    [Authorize]
    public class ApplicationController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICreditScoreService _creditScoreService;
        private readonly ILogger<ApplicationController> _logger;
        private readonly ICompletionService _completionService;

        public ApplicationController(ApplicationDbContext dbContext,
            ICreditScoreService creditScoreService,
            ILogger<ApplicationController> logger,
            ICompletionService completionService)
        {
            _dbContext = dbContext;
            _creditScoreService = creditScoreService;
            _logger = logger;
            _completionService = completionService;
        }

        [Route("readEula")]
        [HttpPost]
        public async Task UserReadEula([FromBody]ApplicationUser eulaUser)
        {
            // Fetch and save credit score for user if it does not exist in our system
            bool userHasCreditScore = _dbContext.CreditScoreEntries.Any(cse => cse.Regno == eulaUser.Id);
            if (!userHasCreditScore)
            {
                try
                {
                    var usersCreditScore = await _creditScoreService.FetchCreditScore(eulaUser.Id);
                    _dbContext.CreditScoreEntries.Add(usersCreditScore);
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Got an error when fetching credit score for user with ssn {eulaUser.Id}. Error message is : {ex.Message}",null);
                    return;
                }
            }

            var userApplicationConnection =
                _dbContext.ApplicationUserConnections.Include(con => con.Application).FirstOrDefault(auc => auc.User.Id == eulaUser.Id);
            if (userApplicationConnection == null)
            {
                return;
            }

            userApplicationConnection.UserHasAgreedToEula = true;
            _dbContext.Update(userApplicationConnection);
            _dbContext.SaveChanges();

            // Update the completion status of the application
            var application = userApplicationConnection.Application;
            application.PersonalPageCompleted = _completionService.PersonalCompleted(User);
            _dbContext.Update(application);
            _dbContext.SaveChanges();
        }

        
    }
}
