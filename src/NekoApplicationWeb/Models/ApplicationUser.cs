﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NekoApplicationWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string UserDisplayName { get; set; }
    }
}