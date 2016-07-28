using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NekoApplicationWeb.ViewModels.Page.Education;
using NekoApplicationWeb.ViewModels.Page.Personal;

namespace NekoApplicationWeb.Controllers.api
{
    [Route("api/degree")]
    [Authorize]
    public class EducationController : Controller
    {
        [Route("list")]
        [HttpPost]
        public void SaveList([FromBody]List<ApplicantDegreesViewModel> vm)
        {
            // TODO save the list
            foreach (var applicantDegreesViewModel in vm)
            {
                Debug.WriteLine(applicantDegreesViewModel);
            }
        }

        [Route("new")]
        [HttpGet]
        public DegreeViewModel EmptyApplicant()
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
