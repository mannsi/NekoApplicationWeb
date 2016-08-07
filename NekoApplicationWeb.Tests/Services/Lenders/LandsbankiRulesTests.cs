using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.Services;
using Xunit;

namespace NekoApplicationWeb.Tests.Services.Lenders
{
    public class LandsbankiRulesTests
    {
        private Lender Landsbankinn => new Lender { Id = NekoApplicationWeb.Shared.Constants.LandsbankinnId };

        [Fact]
        public void Exactly30Percent_ShouldBeOk()
        {
            var service = new LenderService();
            var result = service.VerifyLenderRules(Landsbankinn, 100, 30);
            Assert.False(result.RulesBroken);
        }

        [Fact]
        public void MoreThan30Percent_ShouldBeBrokenRule()
        {
            var service = new LenderService();
            var result = service.VerifyLenderRules(Landsbankinn, 100, 31);
            Assert.True(result.RulesBroken);
        }
    }
}
