using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.Models
{
    public class Lender
    {
        public Lender()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int LoanPaymentServiceFee { get; set; }
    }
}
