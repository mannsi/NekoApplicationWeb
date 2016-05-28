using System;
using System.Collections.Generic;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ViewModels;
using NekoApplicationWeb.ViewModels.Page;

namespace NekoApplicationWeb.Controllers.api
{
    [Route("api")]
    [Authorize]
    public class PageApiController : Controller
    {

        [Route("applicants")]
        [HttpGet]
        public List<Applicant> Personal()
        {
            var result = new List<Applicant>
            {
                new Applicant
                {
                    Legend = "Umsækjandi",
                    Email = "test@testEmail.com",
                    Name = "Mark",
                    Ssn = "1234567899",
                    FacebookPath = "facebook.com/TheZuck"
                }
            };

            return result;
        }
    }
}
