using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.ViewModels.Page.Loan
{
    public class LoanViewModel
    {
        [Required]
        public int BuyingPrice { get; set; }
        [Required]
        public string PropertyNumber { get; set; }
        [Required]
        public int OwnCapital { get; set; }
    
        public string LenderId{ get; set; }


    }
}
