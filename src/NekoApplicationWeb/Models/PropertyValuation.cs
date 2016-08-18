using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.Models
{
    public class PropertyValuation
    {
        [Key]
        public string PropertyNumber { get; set; }
        public int RealEstateValuation2016 { get; set; }
        public int RealEstateValuation2017 { get; set; }
        public int NewFireInsuranceValuation { get; set; }
        public int PlotAssessmentValue { get; set; }
    }
}
