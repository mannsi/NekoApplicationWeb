﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.Models
{
    public class Application
    {
        public Application()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public DateTime TimeCreated { get; set; }
        public ApplicationUser CreatedByUser { get; set; }
        public Lender Lender { get; set; }
    }
}
