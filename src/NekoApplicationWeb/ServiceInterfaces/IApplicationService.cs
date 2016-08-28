using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.ServiceInterfaces
{
    public interface IApplicationService
    {
        Application ActiveApplication(ClaimsPrincipal loggedInUserPrincipal);
    }
}
