using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using NUglify.Html;

namespace NekoApplicationWeb.Shared
{
    public static class ExtensionMethods
    {
        public static bool IsValidSsn(this string ssn)
        {
            if (ssn.Length != 10)
            {
                return false;
            }

            bool allNumbers = ssn.All(Char.IsDigit);
            if (!allNumbers)
            {
                return false;
            }

            return true;
        }

        public static bool IsInteger(this string str)
        {
            int temp;
            return int.TryParse(str, out temp);
        }

        public static int SsnToAge(this string ssn, DateTime now)
        {
            string cleanSsn = ssn.Replace("-", "").Replace(" ","");

            if (!cleanSsn.IsValidSsn()) return -1;

            DateTime dateOfBirth;
            string day = cleanSsn.Substring(0, 2);
            string month = cleanSsn.Substring(2, 2);
            string year = cleanSsn.Substring(4, 2);
            string yearPrefix;
            string lastLetter = cleanSsn.Substring(9, 1);
            if (lastLetter == "8")
            {
                yearPrefix = "18";
            }
            else if(lastLetter == "9")
            {
                yearPrefix = "19";
            }
            else if (lastLetter == "0")
            {
                yearPrefix = "20";
            }
            else
            {
                return -1;
            }

            bool legalDateOfBirth = DateTime.TryParse($"{day}.{month}.{yearPrefix}{year}", out dateOfBirth);

            if (!legalDateOfBirth){ return -1;}

            int years = now.Year - dateOfBirth.Year;

            // Now subtract if birthday has not occured
            if (now.DayOfYear < dateOfBirth.DayOfYear)
            {
                years--;
            }

            return years;
        }
    }
}
