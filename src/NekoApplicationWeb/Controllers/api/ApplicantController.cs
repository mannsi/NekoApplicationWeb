using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NekoApplicationWeb.ViewModels.Page.Personal;

namespace NekoApplicationWeb.Controllers.api
{
    [Route("api/applicant")]
    [Authorize]
    public class ApplicantController : Controller
    {
        [Route("list")]
        [HttpPost]
        public void SaveList([FromBody]PersonalViewModel vm)
        {
            // TODO save the list
            //foreach (var applicantViewModel in vm)
            //{
            //    Debug.WriteLine(applicantViewModel);
            //}
        }
        
    }
}
