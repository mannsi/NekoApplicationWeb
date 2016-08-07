using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.Models
{
    public class CostOfLivingEntry
    {
        public CostOfLivingEntry()
        {
            Id = Guid.NewGuid().ToString();
        }

        public CostOfLivingEntry(int numberOfAdults, 
            int numberOfKidsInKindergarten, 
            int numberOfKidsInElementarySchool, 
            int costOfLivingWithoutTransportationAndHousing, 
            int transportationCostIfNoCar)
        {
            NumberOfAdults = numberOfAdults;
            NumberOfKidsInKindergarten = numberOfKidsInKindergarten;
            NumberOfKidsElementarySchool = numberOfKidsInElementarySchool;
            CostOfLivingWithoutTransportationAndHousing = costOfLivingWithoutTransportationAndHousing;
            TransportationCostIfNoCar = transportationCostIfNoCar;
        }

        public string Id { get; set; }

        public int NumberOfAdults { get; set; }
        public int NumberOfKidsInKindergarten { get; set; }
        public int NumberOfKidsElementarySchool { get; set; }
        public int CostOfLivingWithoutTransportationAndHousing { get; set; }
        public int TransportationCostIfNoCar { get; set; }
    }
}
