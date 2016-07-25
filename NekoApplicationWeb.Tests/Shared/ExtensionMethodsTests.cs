using System;
using System.Collections.Generic;
using System.Linq;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.Services;
using NekoApplicationWeb.Shared;
using NekoApplicationWeb.ViewModels.Page.Loan;
using Xunit;

namespace NekoApplicationWeb.Tests.Shared
{
    public class ExtensionMethodsTests
    {
        [Fact]
        public void TestIsStringIsInteger()
        {
            Assert.True("123".IsInteger());
            Assert.True("0".IsInteger());
            Assert.False("abc".IsInteger());
            Assert.False("1a2".IsInteger());
        }

        [Fact]
        public void TestSsnAge()
        {
            int myAge = "2302835679".SsnToAge(new DateTime(2016, 7, 25));
            Assert.Equal(33, myAge);

            int myAgeBeforeMyBirthday = "2302835679".SsnToAge(new DateTime(2016, 2, 22));
            Assert.Equal(32, myAgeBeforeMyBirthday);

            int myAgeBeforeOnBirthday = "2302835679".SsnToAge(new DateTime(2016, 2, 23));
            Assert.Equal(33, myAgeBeforeOnBirthday);
        }
    }
}
