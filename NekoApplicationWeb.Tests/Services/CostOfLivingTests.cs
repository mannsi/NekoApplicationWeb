using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.Services;
using Xunit;

namespace NekoApplicationWeb.Tests.Services
{
    public class CostOfLivingTests
    {
        [Fact]
        public void VsPrecalculatedTest()
        {
            var service = new CostOfLivingService();
            int expectedCost = 332862;

            int numAdults = 1;
            int numPreSchoolKids = 1;
            int numElementarySchoolKids = 0;
            int numberOfCars = 2;
            int buyingPrice = 30000000;

            int costOfLivingWithoutHouseAndTransport = 158758;
            int transportationCostWithoutCar = 21800;
            var costOfLivingEntries = new List<CostOfLivingEntry>
            {
                new CostOfLivingEntry(1,1,1,1,1), // Fake line
                new CostOfLivingEntry(numAdults, numPreSchoolKids, numElementarySchoolKids, costOfLivingWithoutHouseAndTransport, transportationCostWithoutCar), // Real line
            };

            var calculatedCost = service.GetCostOfLivingWithoutLoans(costOfLivingEntries, numAdults, numPreSchoolKids, numElementarySchoolKids, numberOfCars, buyingPrice);

            Assert.Equal(expectedCost, calculatedCost);
        }

        [Fact]
        public void NoAdultShouldThrowTest()
        {
            var service = new CostOfLivingService();

            int numAdults = 0;

            Assert.Throws<Exception>(() =>
            {
                service.GetCostOfLivingWithoutLoans(new List<CostOfLivingEntry>(), numAdults, 0, 0, 0, 0);
            });
        }

        [Fact]
        public void NoCarTest()
        {
            var service = new CostOfLivingService();
            int expectedCost = 230558;

            int numAdults = 1;
            int numPreSchoolKids = 1;
            int numElementarySchoolKids = 0;
            int numberOfCars = 0;
            int buyingPrice = 30000000;

            int costOfLivingWithoutHouseAndTransport = 158758;
            int transportationCostWithoutCar = 21800;
            var costOfLivingEntries = new List<CostOfLivingEntry>
            {
                new CostOfLivingEntry(1,1,1,1,1), // Fake line
                new CostOfLivingEntry(numAdults, numPreSchoolKids, numElementarySchoolKids, costOfLivingWithoutHouseAndTransport, transportationCostWithoutCar), // Real line
            };

            var calculatedCost = service.GetCostOfLivingWithoutLoans(costOfLivingEntries, numAdults, numPreSchoolKids, numElementarySchoolKids, numberOfCars, buyingPrice);

            Assert.Equal(expectedCost, calculatedCost);
        }
    }
}
