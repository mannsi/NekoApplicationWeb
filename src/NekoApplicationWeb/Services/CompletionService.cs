using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace NekoApplicationWeb.Services
{
    public class CompletionService : ICompletionService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public CompletionService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        private Application ActiveApplication(ClaimsPrincipal loggedInUserPrincipal)
        {
            var loggedInUserId = _userManager.GetUserId(loggedInUserPrincipal);
            var userConnection = _dbContext.ApplicationUserConnections.Include(con => con.Application).ThenInclude(application => application.Lender).FirstOrDefault(con => con.User.Id == loggedInUserId);

            return userConnection?.Application;
        }

        private List<ApplicationUser> ApplicantsInActiveApplication(ClaimsPrincipal loggedInUserPrincipal)
        {
            var loggedInUserId = _userManager.GetUserId(loggedInUserPrincipal);
            var userConnection = _dbContext.ApplicationUserConnections.Include(con => con.Application).FirstOrDefault(con => con.User.Id == loggedInUserId);

            return
                _dbContext.ApplicationUserConnections.Include(con => con.Application)
                    .Where(con => con.Application == userConnection.Application).Select(con => con.User).ToList();
        }

        public bool PersonalCompleted(ClaimsPrincipal loggedInUserPrincipal)
        {
            var applicationUsers = ApplicantsInActiveApplication(loggedInUserPrincipal);

            foreach (var applicationUser in applicationUsers)
            {
                if (string.IsNullOrEmpty(applicationUser.Email)) return false;
                var userHasConfirmedEula =
                    _dbContext.ApplicationUserConnections.First(con => con.User == applicationUser).UserHasAgreedToEula;
                if (!userHasConfirmedEula) return false;
            }

            return true;
        }

        public bool EducationCompleted(ClaimsPrincipal loggedInUserPrincipal)
        {
            var applicationUsers = ApplicantsInActiveApplication(loggedInUserPrincipal);

            foreach (var applicationUser in applicationUsers)
            {
                var applicantEducations = _dbContext.ApplicantEducations.Where(edu => edu.User == applicationUser);
                foreach (var applicantEducation in applicantEducations)
                {
                    if (string.IsNullOrEmpty(applicantEducation.Degree) || 
                        string.IsNullOrEmpty(applicantEducation.School))
                    {
                        return false;
                    }       
                }
            }

            return true;
        }

        public bool EmploymentCompleted(ClaimsPrincipal loggedInUserPrincipal)
        {
            var applicationUsers = ApplicantsInActiveApplication(loggedInUserPrincipal);

            foreach (var applicationUser in applicationUsers)
            {
                var applicantEmployments = _dbContext.ApplicantEmployments.Where(edu => edu.User == applicationUser);
                foreach (var applicantEmployment in applicantEmployments)
                {
                    if (string.IsNullOrEmpty(applicantEmployment.Title) ||
                        string.IsNullOrEmpty(applicantEmployment.Company))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool FinancesCompleted(ClaimsPrincipal loggedInUserPrincipal)
        {
            var applicationUsers = ApplicantsInActiveApplication(loggedInUserPrincipal);

            foreach (var applicationUser in applicationUsers)
            {
                var applicantIncomes = _dbContext.ApplicantIncomes.Where(edu => edu.User == applicationUser);
                foreach (var applicantIncome in applicantIncomes)
                {
                    if (applicantIncome.MonthlyAmount == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool LoanCompleted(ClaimsPrincipal loggedInUserPrincipal)
        {
            var application = ActiveApplication(loggedInUserPrincipal);

            if (application.Lender == null) return false;

            var propertyDetails =_dbContext.PropertyDetails.FirstOrDefault(detail => detail.Application == application);
            if (propertyDetails == null) return false;

            if (propertyDetails.BuyingPrice == 0) return false;
            if (string.IsNullOrEmpty(propertyDetails.PropertyNumber)) return false;

            return true;
        }

        public bool DocumentsCompleted(ClaimsPrincipal loggedInUserPrincipal)
        {
            return false;
        }
    }
}
