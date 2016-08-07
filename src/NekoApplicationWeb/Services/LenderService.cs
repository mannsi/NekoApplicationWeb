using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace NekoApplicationWeb.Services
{
    public class LenderService : ILenderService
    {
        public LenderLendingRule VerifyLenderRules(Lender lender, int totalApplicantMonthlyIncome, int totalLoanPayments)
        {
            bool rulesBroken = false;
            string message = "";

            switch (lender.Id)
            {
                case Shared.Constants.ArionId:
                    break;
                case Shared.Constants.AlmenniLifeyrissjodurinnId:
                    break;
                case Shared.Constants.FrjalsiLifeyrissjodurinnId:
                    break;
                case Shared.Constants.IslandsbankiId:
                    rulesBroken = totalApplicantMonthlyIncome*0.35 < totalLoanPayments;
                    if (rulesBroken)
                    {
                        message = "Íslandsbanki veitir ekki lán ef greiðslur af lánum eru yfir 35% af tekjum";
                    }
                    break;
                case Shared.Constants.LandsbankinnId:
                    rulesBroken = totalApplicantMonthlyIncome * 0.30 < totalLoanPayments;
                    if (rulesBroken)
                    {
                        message = "Landsbankinn veitir ekki lán ef greiðslur af lánum eru yfir 30% af tekjum";
                    }
                    break;
            }

            return new LenderLendingRule { RulesBroken = rulesBroken, RulesBrokenText = message };

        }
    }
}
