using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NekoApplicationWeb.Models
{
    public enum LoanType
    {
        Regular,
        Additional,
        Neko
    }

    public enum InterestsForm
    {
        Variable,
        Fixed
    }

    public enum LoanPaymentType
    {
        Annuitet, // Jafnar greidslur (low payments first, higher later)
        EvenPayments, // Jafnar afborganir (hight payments first, lower later)
        Neko 
    }

    /// <summary>
    /// A single line from and interests matrix provided by a single company (f.x. a bank)
    /// </summary>
    public class InterestsInfo
    {
        public LoanType LoanType { get; set; }
        public bool Indexed { get; set; }
        public int LoanToValueStartPercentage { get; set; }
        public int LoanToValueEndPercentage { get; set; }
        public InterestsForm InterestsForm { get; set; }
        public int FixedInterestsYears { get; set; } // Only applies to fixed interests
        public double InterestPercentage { get; set; }
        public LoanPaymentType LoanPaymentType { get; set; }
        public int LoanTimeYearsMin { get; set; }
        public int LoanTimeYearsMax { get; set; }
    }
}
