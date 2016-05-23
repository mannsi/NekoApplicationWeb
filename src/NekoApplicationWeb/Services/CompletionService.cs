using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NekoApplicationWeb.ServiceInterfaces;

namespace NekoApplicationWeb.Services
{
    public class CompletionService : ICompletionService
    {
        public bool PersonalCompleted()
        {
            return true;
        }

        public bool EducationCompleted()
        {
            return false;
        }

        public bool FinancesCompleted()
        {
            return false;
        }

        public bool LoanCompleted()
        {
            return false;
        }

        public bool EmploymentCompleted()
        {
            return false;
        }

        public bool DocumentsCompleted()
        {
            return false;
        }
    }
}
