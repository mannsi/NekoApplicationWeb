using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.Services;
using Xunit;

namespace NekoApplicationWeb.Tests.Services.Lenders
{
    public class IslandsBankiRulesTests
    {
        private Lender Islandsbanki => new Lender { Id = NekoApplicationWeb.Shared.Constants.IslandsbankiId };

        [Fact]
        public void Exactly35Percent_ShouldBeOk()
        {
            var service = new LenderService();
            var result = service.VerifyLenderRules(Islandsbanki, 100, 35);
            Assert.False(result.RulesBroken);
        }

        [Fact]
        public void MoreThan35Percent_ShouldBeBrokenRule()
        {
            var service = new LenderService();
            var result = service.VerifyLenderRules(Islandsbanki, 100, 36);
            Assert.True(result.RulesBroken);
        }
    }
}
