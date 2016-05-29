using System.Collections.Generic;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using NekoApplicationWeb.ViewModels.Page.Personal;

namespace NekoApplicationWeb.Controllers.api
{
    [Route("api")]
    [Authorize]
    public class PageApiController : Controller
    {

        [Route("applicant/list")]
        [HttpGet]
        public List<ApplicantViewModel> Personal()
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

        [Route("applicant/new")]
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
