using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using NekoApplicationWeb.ViewModels.Page.Education;
using NekoApplicationWeb.ViewModels.Page.Employment;
using NekoApplicationWeb.ViewModels.Page.Finances;
using NekoApplicationWeb.ViewModels.Page.Personal;

namespace NekoApplicationWeb.Controllers.api
{
    [Route("api/finances")]
    [Authorize]
    public class FinancesController : Controller
    {
        [Route("list")]
        [HttpPost]
        public void SaveList([FromBody]List<ApplicantFinancesViewModel> vm)
        {
            // TODO save the list
            foreach (var applicantDegreesViewModel in vm)
            {
                Debug.WriteLine(applicantDegreesViewModel);
            }
        }
    }
}
