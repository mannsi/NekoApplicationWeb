using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NekoApplicationWeb.Models;
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

        public EducationController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
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
                var userDegrees = _dbContext.ApplicationEducations.Where(deg => deg.User == user);
                _dbContext.ApplicationEducations.RemoveRange(userDegrees);
            }
            _dbContext.SaveChanges();

            // Save all the degrees
            foreach (var degreesVm in vm)
            {
                foreach (var degreeVm in degreesVm.Degrees)
                {
                    _dbContext.ApplicationEducations.Add(new ApplicantEducation
                    {
                        Degree = degreeVm.Degree,
                        School = degreeVm.School,
                        User = degreesVm.Applicant,
                        FinishingDate = degreeVm.DateFinished
                    });
                }
            }

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
