using System.Collections.Generic;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.ServiceInterfaces
{
    public interface ICostOfLivingService
    {
        int GetCostOfLivingWithoutLoans(
            List<CostOfLivingEntry> costOfLivingEntries,
            int numberOfAdults,
            int numberOfPreSchoolKids,
            int numberOfElementarySchoolKids,
            int numberOfCars,
            int buyingPriceOfProperty);
    }
}