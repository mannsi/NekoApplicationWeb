using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ServiceInterfaces;
using NekoApplicationWeb.ViewModels.Page.Loan;
using MoreLinq;

namespace NekoApplicationWeb.Services
{
    public class PropertyValuationService : IPropertyValuationService
    {
        private readonly ApplicationDbContext _dbContext;

        public PropertyValuationService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PropertyValuation GetPropertyValuation(string propertyNumber)
        {
            propertyNumber = propertyNumber?.Replace("-", "");

            return _dbContext.PropertyValuations.FirstOrDefault(prop => prop.PropertyNumber == propertyNumber);
        }
    }
}
