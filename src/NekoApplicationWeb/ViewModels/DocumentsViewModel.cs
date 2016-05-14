using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace NekoApplicationWeb.ViewModels
{
    public class DocumentsViewModel
    {
        [Required(ErrorMessage = "Verður að fylla")]
        [Display(Name = "Launaseðill 1")]
        public IFormFile PayCheck1 { get; set; }
    }
}
