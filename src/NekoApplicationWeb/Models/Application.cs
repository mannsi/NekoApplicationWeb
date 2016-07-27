using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.Models
{
    public class Application
    {
        public string Id { get; set; }
        public DateTime TimeCreated { get; set; }
        public ApplicationUser CreatedByUser { get; set; }
    }
}
