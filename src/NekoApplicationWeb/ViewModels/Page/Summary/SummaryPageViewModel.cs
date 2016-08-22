using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.ViewModels.Page.Summary
{
    public class SummaryPageViewModel
    {
        public SummaryPageViewModel()
        {
            ListOfErrorMessage = new List<string>();
        }

        public List<string> ListOfErrorMessage { get; set; }
    }
}
