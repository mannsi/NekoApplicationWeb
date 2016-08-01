using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ServiceInterfaces;
using NekoApplicationWeb.Shared;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace NekoApplicationWeb.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(
            ApplicationDbContext dbContext, 
            UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> CreateUser(string ssn, string userName, bool isDeletable)
        {
            ssn = ssn.CleanSsn();

            // Make sure we are not adding somebody that already exists in the database
            if (_dbContext.Users.Any(u => u.Id == ssn))
            {
                return null;
            }

            var user = new ApplicationUser
            {
                Id = ssn,
                UserName = userName,
                IsDeletable = isDeletable
            };

            await _userManager.CreateAsync(user);

            _dbContext.ApplicationEducations.Add(new ApplicantEducation
            {
                User = user,
                FinishingDate = DateTime.Now
            });
            _dbContext.ApplicantEmployments.Add(new ApplicantEmployment {User = user, StartingTime = DateTime.Now});
            _dbContext.SaveChanges();

            return user;
        }
    }
}
