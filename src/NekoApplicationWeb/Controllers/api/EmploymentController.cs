using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ServiceInterfaces;
using NekoApplicationWeb.ViewModels.Page.Employment;

namespace NekoApplicationWeb.Controllers.api
{
    [Route("api/employment")]
    [Authorize]
    public class EmploymentController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompletionService _completionService;

        public EmploymentController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, ICompletionService completionService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _completionService = completionService;
        }

        [Route("list")]
        [HttpPost]
        public async Task SaveList([FromBody]List<ApplicantEmploymentViewModel> vmList)
        {
            foreach (var vm in vmList)
            {
                var userEmployement = _dbContext.ApplicantEmployments.FirstOrDefault(empl => empl.User == vm.Applicant);
                if (userEmployement == null) return;

                _dbContext.Update(userEmployement);

                userEmployement.Company = vm.CompanyName;
                userEmployement.Title = vm.Title;
                userEmployement.StartingTime = vm.From;
            }

            _dbContext.SaveChanges();

            // Update the completion status of the application
            var loggedInUser = await _userManager.GetUserAsync(User);
            var application = PageController.GetApplicationForUser(loggedInUser, _dbContext);
            application.EmploymentPageCompleted = _completionService.EmploymentCompleted(User);
            _dbContext.Update(application);
            _dbContext.SaveChanges();
        }
    }
}
