﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.ViewModels.Page.Start
{
    public class StartPageViewModel
    {
        public bool ShowEula { get; set; }
        public ApplicationUser EulaUser { get; set; }
    }
}
