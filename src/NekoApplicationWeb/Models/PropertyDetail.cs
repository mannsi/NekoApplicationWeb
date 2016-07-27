using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.Models
{
    public class PropertyDetail
    {
        public string Id { get; set; }
        public Application Application { get; set; }
        public string PropertyNumber { get; set; }
        public int BuyingPrice { get; set; }
        public int OwnCapital { get; set; }
    }
}
