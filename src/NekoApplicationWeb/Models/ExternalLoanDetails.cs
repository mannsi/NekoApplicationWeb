using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.Models
{
    public class ExternalLoanDetails
    {
        public ExternalLoanDetails()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public Application Application { get; set; }
        public Lender Lender { get; set; }
    }
}
