using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.PeerResolvers;
using System.Threading.Tasks;
using System.Xml.Linq;
using NekoApplicationWeb.Shared;

namespace NekoApplicationWeb.Models
{
    public class ThjodskraFamilyEntry
    {
        public string Id { get; set; }
        public DateTime TimeOfData { get; set; } // When the data was last fetched
        public string FamilyNumber { get; set; }
        public string Ssn { get; set; }
        public string Name { get; set; }
        public GenderCode GenderCode { get; set; }

        public static List<ThjodskraFamilyEntry> FromXml(string xmlString)
        {
            XNamespace xmlNameSpace = "http://tempuri.org/";

            XElement xmlDoc = XElement.Parse(xmlString);

            bool isFound = Convert.ToBoolean(xmlDoc.Element(xmlNameSpace + "FANNST")?.Value);
            if (isFound)
            {
                var resultNodes = xmlDoc.Element(xmlNameSpace + "NIDURSTADA")?.Elements(xmlNameSpace + "anyType");

                if (resultNodes != null)
                {
                    List<ThjodskraFamilyEntry> familyMembers = new List<ThjodskraFamilyEntry>();

                    foreach (var element in resultNodes)
                    {
                        string ssn = element.Element(xmlNameSpace + "KENNITALA")?.Value;
                        string name = element.Element(xmlNameSpace + "NAFN")?.Value;
                        string genderCodeString = element.Element(xmlNameSpace + "KYNKODI")?.Value;

                        GenderCode genderCode = GenderCode.Invalid;
                        if (genderCodeString.IsInteger())
                        {
                            genderCode = (GenderCode)Convert.ToInt32(genderCodeString);
                        }

                        var member = new ThjodskraFamilyEntry
                        {
                            Ssn = ssn,
                            Name = name,
                            GenderCode = genderCode
                        };

                        familyMembers.Add(member);
                    }

                    return familyMembers;
                }
            }

            return null;
        }
    }
}
