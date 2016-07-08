using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NekoApplicationWeb.ViewModels.Page.Employment
{
    public class ApplicantEmployment
    {
        public string ApplicantName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string CompanyName { get; set; }

        [DataType(DataType.Date)]
        public DateTime From { get; set; }
    }
}
