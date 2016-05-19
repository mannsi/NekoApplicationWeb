using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Http;

namespace NekoApplicationWeb.ViewModels.Page
{
    public class DocumentsViewModel
    {
        [Required(ErrorMessage = "Verður að fylla")]
        [Display(Name = "Launaseðill 1")]
        public IFormFile PayCheck1 { get; set; }
    }
}
