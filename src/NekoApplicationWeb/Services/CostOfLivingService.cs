using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using NekoApplicationWeb.Shared;

namespace NekoApplicationWeb.Services
{
    public class CostOfLivingService : ICostOfLivingService
    {
        private const int CostPerCar = 62052;
        private const double HousingCostRatio = 0.02/12;

        public int GetCostOfLivingWithoutLoans(
            List<CostOfLivingEntry> costOfLivingEntries, 
            int numberOfAdults, 
            int numberOfPreSchoolKids, 
            int numberOfElementarySchoolKids, 
            int numberOfCars, 
            int buyingPriceOfProperty)
        {
            int costOfLiving = 0;

            if (numberOfAdults > 2)
            {
                // Cost tables do not handle more than 2 adults
                numberOfAdults = 2;
            }

            if (numberOfAdults == 0)
            {
                // Should not happen
                throw new Exception("No adult in family list");
            }

            if (numberOfPreSchoolKids > 4)
            {
                // Cost tables do not handle more than 4 preschoolers
                numberOfPreSchoolKids = 4;
            }

            if (numberOfElementarySchoolKids > 4)
            {
                // Cost tables do not handle more than 4 kids in elementary school
                numberOfElementarySchoolKids = 4;
            }

            var costOfLivingEntry = costOfLivingEntries.FirstOrDefault(entry =>
                entry.NumberOfAdults == numberOfAdults &&
                entry.NumberOfKidsInKindergarten == numberOfPreSchoolKids &&
                entry.NumberOfKidsElementarySchool == numberOfElementarySchoolKids);
            if (costOfLivingEntry == null)
            {
                throw new Exception("No cost of living entry found for family");
            }

            costOfLiving += costOfLivingEntry.CostOfLivingWithoutTransportationAndHousing;

            int transportationCost;
            if (numberOfCars == 0)
            {
                transportationCost = costOfLivingEntry.TransportationCostIfNoCar;
            }
            else
            {
                transportationCost = CostPerCar*numberOfCars;
            }

            costOfLiving += transportationCost;

            int housingCost = (int)(buyingPriceOfProperty*HousingCostRatio);
            costOfLiving += housingCost;

            return costOfLiving;
        }
    }
}
