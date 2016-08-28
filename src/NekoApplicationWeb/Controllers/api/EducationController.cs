using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ServiceInterfaces;
using NekoApplicationWeb.ViewModels.Page.Education;
using NekoApplicationWeb.ViewModels.Page.Personal;

namespace NekoApplicationWeb.Controllers.api
{
    [Route("api/degree")]
    [Authorize]
    public class EducationController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompletionService _completionService;

        public EducationController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, ICompletionService completionService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _completionService = completionService;
        }

        [Route("list")]
        [HttpPost]
        public async Task SaveList([FromBody]List<ApplicantDegreesViewModel> vm)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            var userConnections = PageController.GetUsersConnectionsForUsersApplication(loggedInUser, _dbContext);

            // Remove previous degree entries
            foreach (var userConnection in userConnections)
            {
                var user = userConnection.User;
                var userDegrees = _dbContext.ApplicantEducations.Where(deg => deg.User == user);
                _dbContext.ApplicantEducations.RemoveRange(userDegrees);
            }
            _dbContext.SaveChanges();

            // Save all the degrees
            foreach (var degreesVm in vm)
            {
                foreach (var degreeVm in degreesVm.Degrees)
                {
                    _dbContext.ApplicantEducations.Add(new ApplicantEducation
                    {
                        Degree = degreeVm.Degree,
                        School = degreeVm.School,
                        User = degreesVm.Applicant,
                        FinishingDate = degreeVm.DateFinished
                    });
                }
            }

            _dbContext.SaveChanges();

            // Update the completion status of the application
            var application = PageController.GetApplicationForUser(loggedInUser, _dbContext);
            application.EducationPageCompleted = _completionService.EducationCompleted(User);
            _dbContext.Update(application);
            _dbContext.SaveChanges();
        }

        [Route("new")]
        [HttpGet]
        public DegreeViewModel EmptyDegree()
        {
            return new DegreeViewModel
            {
                Degree = "",
                School = "",
                DateFinished = DateTime.Now
            };
        }
    }
}
