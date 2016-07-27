using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ServiceInterfaces;

namespace NekoApplicationWeb.Services
{
    public class InterestsService : IInterestsService
    {
        private readonly ApplicationDbContext _dbContext;

        public InterestsService(ApplicationDbContext dbContext)
        {
            // Interests are temp stored in database until a web service is ready to server the data
            _dbContext = dbContext;
        }

        public List<InterestsEntry> GetInterestsMatrix(Lender lender)
        {
            var interestLines = _dbContext.InterestsEntries.Where(ie => ie.Lender == lender).ToList();

            var nekoInterests = GetNekoInterestInfo();
            interestLines.Add(nekoInterests);

            return interestLines;
        }

        private InterestsEntry GetNekoInterestInfo()
        {
            var nekoLender = _dbContext.Lenders.First(lender => lender.Id == Shared.Constants.NekoLenderId);
            return _dbContext.InterestsEntries.First(ie => ie.Lender == nekoLender);
        }
    }
}
