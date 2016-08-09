using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CreditScore;

namespace NekoApplicationWeb.Models
{
    public class CreditScoreEntry
    {
        public CreditScoreEntry()
        {
            Id = Guid.NewGuid().ToString();
        }

        [NotMapped]
        public bool CouldCalculateCreditScore => Scorestatusid == 0;

        public string Id { get; set; }
        public DateTime TimeOfData { get; set; } // When the data was fetched
        public string Regno { get; set; } // Users ssn
        public string Name { get; set; }
        public string Scoreband { get; set; }
        public double PD { get; set; }
        public int Points { get; set; }
        public int Scorestatusid { get; set; }
        public string Scorestatusdescription { get; set; }
        public string ScorestatusdescriptionEN { get; set; }
        public bool CompanyRelation { get; set; }
        public float? RatioBelowAll { get; set; }
        public float? RatioBelowAgeGrp { get; set; }
        public float? RatioBelowLocation { get; set; }
        public int AgeGrp { get; set; }
        public string RegionCode { get; set; }

        public static CreditScoreEntry FromWebServiceObject(CIPScore serviceObj)
        {
            var entry = new CreditScoreEntry
            {
                TimeOfData = DateTime.Now,
                Regno = serviceObj.Regno,
                Name = serviceObj.Name,
                Scoreband = serviceObj.Scoreband,
                PD = serviceObj.PD,
                Points = serviceObj.Points,
                Scorestatusid = serviceObj.Scorestatusid,
                Scorestatusdescription = serviceObj.Scorestatusdescription,
                ScorestatusdescriptionEN = serviceObj.ScorestatusdescriptionEN,
                CompanyRelation = serviceObj.CompanyRelation,
                RatioBelowAll = serviceObj.RatioBelowAll,
                RatioBelowAgeGrp = serviceObj.RatioBelowAgeGrp,
                RatioBelowLocation = serviceObj.RatioBelowLocation,
                AgeGrp = serviceObj.AgeGrp,
                RegionCode = serviceObj.RegionCode
            };

            return entry;
        }
    }
}
