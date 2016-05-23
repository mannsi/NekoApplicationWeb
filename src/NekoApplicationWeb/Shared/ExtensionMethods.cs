using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.Shared
{
    public static class ExtensionMethods
    {
        public static bool IsValidSsn(this string ssn)
        {
            return ssn.Length == 10;
        }
    }
}
