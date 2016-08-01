using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ViewModels.Page.Employment;

namespace NekoApplicationWeb.Controllers.api
{
    [Route("api/employment")]
    [Authorize]
    public class EmploymentController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public EmploymentController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("list")]
        [HttpPost]
        public void SaveList([FromBody]List<ApplicantEmploymentViewModel> vmList)
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
        }
    }
}
