using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using NekoApplicationWeb.ViewModels.Page.Personal;

namespace NekoApplicationWeb.Controllers.api
{
    [Route("api/applicant")]
    [Authorize]
    public class Applicant : Controller
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
        public void SaveList([FromBody]List<ApplicantViewModel> vm)
        {
            // TODO save the list
            foreach (var applicantViewModel in vm)
            {
                Debug.WriteLine(applicantViewModel);
            }
        }

        [Route("new")]
        [HttpGet]
        public ApplicantViewModel EmptyApplicant()
        {
            return new ApplicantViewModel
            {
                Email = "",
                Name = "",
                Ssn = "",
                FacebookPath = ""
            };
        }

        
    }
}
