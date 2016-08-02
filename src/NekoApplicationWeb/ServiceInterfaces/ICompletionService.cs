using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NekoApplicationWeb.ServiceInterfaces
{
    public interface ICompletionService
    {
        bool PersonalCompleted(ClaimsPrincipal loggedInUserPrincipal);
        bool EducationCompleted(ClaimsPrincipal loggedInUserPrincipal);
        bool FinancesCompleted(ClaimsPrincipal loggedInUserPrincipal);
        bool LoanCompleted(ClaimsPrincipal loggedInUserPrincipal);
        bool EmploymentCompleted(ClaimsPrincipal loggedInUserPrincipal);
        bool DocumentsCompleted(ClaimsPrincipal loggedInUserPrincipal);
    }
}
