using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace NekoApplicationWeb.Models
{
    public enum DebtType
    {
        StudentLoan,
        Overdraft,
        CarLoan,
        PropertyLoan,
        Other
    }

    public class Debt
    {
        public Debt()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public Application Application { get; set; }

        public DebtType DebtType { get; set; }
        public string Lender { get; set; }
        public int LoanRemains { get; set; }
        public int MonthlyPayment { get; set; }
    }
}
