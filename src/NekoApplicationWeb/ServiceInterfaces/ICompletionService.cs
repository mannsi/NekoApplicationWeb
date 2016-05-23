using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.ServiceInterfaces
{
    public interface ICompletionService
    {
        bool PersonalCompleted();
        bool EducationCompleted();
        bool FinancesCompleted();
        bool LoanCompleted();
        bool EmploymentCompleted();
        bool DocumentsCompleted();
    }
}
