using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.Models
{
    public class PropertyValuation
    {
        public PropertyValuation()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public Application Application { get; set; }
        public DateTime TimeOfData { get; set; } // When the data was last fetched
        public int RealEstateValuation;
        public int NewFireInsuranceValuation;
        public int PlotAssessmentValue;
    }
}
