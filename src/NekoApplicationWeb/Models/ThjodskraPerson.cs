using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using NekoApplicationWeb.Shared;

namespace NekoApplicationWeb.Models
{
    public enum GenderCode
    {
        Invalid = -1,
        Male18YearsOrOlder = 1,
        Female18YearsOrOlder = 2,
        Male17YearsOrYounger = 3,
        Female17YearsOrYounger = 4
    }

    public enum MaritalStatus
    {
        Invalid=-1,
        Unmarried = 1,
        MarriedOrConfirmedRelationship = 3,
        EkkillOrEkkja=4,
        Separated=5,
        Divorced=6,
        HjonEkkiISamvistum=7,
        IcelandicMarriedToForeigner=8,
        Unknown=9,
        IcelandicWithAForeignAddress=0,
        // L = Íslendingur með lögheimili á Íslandi (t.d. námsmaður eða sendiráðsmaður); í hjúskap með útlendingi sem ekki er á skrá. WTF L ???
    }

    public class ThjodskraPerson
    {
        public string Id { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime TimeOfData { get; set; } // When the data was last fetched
        public string Name { get; set; }
        public string Home { get; set; }
        public string PostCode { get; set; }
        public string FamilyNumber { get; set; }
        public GenderCode GenderCode { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public string PlaceOfResidence { get; set; }
        public string SpouseSsn { get; set; }

        public static ThjodskraPerson FromXml(string xmlString)
        {
            XNamespace xmlNameSpace = "http://tempuri.org/";
            XElement xmlDoc = XElement.Parse(xmlString);

            bool isFound = Convert.ToBoolean(xmlDoc.Element(xmlNameSpace + "FANNST")?.Value);

            if (isFound)
            {
                var resultNode = xmlDoc.Element(xmlNameSpace + "NIDURSTADA")?.Element(xmlNameSpace + "anyType");

                if (resultNode != null)
                {
                    string ssn = resultNode.Element(xmlNameSpace + "KENNITALA")?.Value;
                    string name = resultNode.Element(xmlNameSpace + "NAFN")?.Value;
                    string home = resultNode.Element(xmlNameSpace + "HEIMILI")?.Value;
                    string postCode = resultNode.Element(xmlNameSpace + "POSTNUMER")?.Value;
                    string familyNumber = resultNode.Element(xmlNameSpace + "FJOLSKYLDUNR")?.Value;
                    string genderCodeString = resultNode.Element(xmlNameSpace + "KYNKODI")?.Value;
                    string maritalStatusString = resultNode.Element(xmlNameSpace + "HJUSKAPUR")?.Value;
                    string placeOfResidence = resultNode.Element(xmlNameSpace + "LOGHEIMILI_1_12")?.Value;
                    string spouseSsn = resultNode.Element(xmlNameSpace + "KENNITALA_MAKA")?.Value;

                    GenderCode genderCode = GenderCode.Invalid;
                    if (genderCodeString.IsInteger())
                    {
                        genderCode = (GenderCode) Convert.ToInt32(genderCodeString);
                    }

                    MaritalStatus maritalStatus = MaritalStatus.Invalid;
                    if (maritalStatusString.IsInteger())
                    {
                        maritalStatus = (MaritalStatus)Convert.ToInt32(maritalStatusString);
                    }

                    var person = new ThjodskraPerson
                    {
                        Id = ssn,
                        Name = name,
                        Home = home,
                        PostCode = postCode,
                        FamilyNumber = familyNumber,
                        GenderCode = genderCode,
                        MaritalStatus = maritalStatus,
                        PlaceOfResidence =  placeOfResidence,
                        SpouseSsn = spouseSsn
                    };

                    return person;
                }
            }

            return null;
        }
    }
}
