using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ViewModels;
using NekoApplicationWeb.ViewModels.Page.Personal;

namespace NekoApplicationWeb.Controllers.api
{
    [Route("api/applicant")]
    [Authorize]
    public class ApplicantController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ApplicantController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("list")]
        [HttpPost]
        public void SaveList([FromBody]List<UserViewModel> vm)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                foreach (var vmUser in vm)
                {
                    var user = _dbContext.Users.FirstOrDefault(u => u.Id == vmUser.Id);
                    if (user == null) continue;

                    _dbContext.Update(user);

                    user.Email = vmUser.Email;
                    user.FacebookPath = vmUser.FacebookPath;
                    user.TwitterPath = vmUser.TwitterPath;
                    user.LinkedInPath = vmUser.LinkedInPath;

                    _dbContext.SaveChanges();
                }

                transaction.Commit();
            }
        }
        
    }
}
