using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NekoApplicationWeb.ViewModels.Page.Employment;

namespace NekoApplicationWeb.Controllers.api
{
    [Route("api/employment")]
    [Authorize]
    public class EmploymentController : Controller
    {
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
