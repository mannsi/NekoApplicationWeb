using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using NekoApplicationWeb.ViewModels.Page.Education;
using NekoApplicationWeb.ViewModels.Page.Employment;
using NekoApplicationWeb.ViewModels.Page.Personal;

namespace NekoApplicationWeb.Controllers.api
{
    [Route("api/employment")]
    [Authorize]
    public class EmploymentController : Controller
    {
        [Route("list")]
        [HttpGet]
        public List<ApplicantViewModel> List()
        {
            var result = new List<ApplicantViewModel>
            {
                new ApplicantViewModel
                {
                    Email = "test@testEmail.com",
                    Name = "Mark",
                    Ssn = "1234567899",
                    FacebookPath = "facebook.com/TheZuck"
                }
            };

            return result;
        }

        [Route("list")]
        [HttpPost]
        public void SaveList([FromBody]List<ApplicantEmployment> vm)
        {
            // TODO save the list
            foreach (var applicantDegreesViewModel in vm)
            {
                Debug.WriteLine(applicantDegreesViewModel);
            }
        }
    }
}
