﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Tools.Internal;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ServiceInterfaces;
using NekoApplicationWeb.Shared;
using NekoApplicationWeb.ViewModels;
using NekoApplicationWeb.ViewModels.Page.Personal;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NekoApplicationWeb.Controllers.api
{
    [Route("api/applicant")]
    [Authorize]
    public class ApplicantController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IThjodskraService _thjodskraService;
        private readonly IUserService _userService;

        public ApplicantController(ApplicationDbContext dbContext, IThjodskraService thjodskraService, IUserService userService)
        {
            _dbContext = dbContext;
            _thjodskraService = thjodskraService;
            _userService = userService;
        }

        [Route("list")]
        [HttpPost]
        public void SaveList([FromBody]List<UserViewModel> vm)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                foreach (var vmUser in vm)
                {
                    var user = _dbContext.Users.FirstOrDefault(u => u.Id == vmUser.Id);
                    if (user == null) continue;

                    _dbContext.Update(user);

                    user.Email = vmUser.Email;
                    user.FacebookPath = vmUser.FacebookPath;
                    user.TwitterPath = vmUser.TwitterPath;
                    user.LinkedInPath = vmUser.LinkedInPath;

                    _dbContext.SaveChanges();
                }

                transaction.Commit();
            }
        }

        //[Route("create")]
        //[HttpPost]
        //public async Task<UserViewModel> Create([FromBody]string ssn)
        //{
        //    // Check if thjodskra entry exists in database
        //    var thjodskraPerson = _dbContext.ThjodskraPersons.FirstOrDefault(p => p.Id == ssn);

        //    if (thjodskraPerson == null)
        //    {
        //        thjodskraPerson = _thjodskraService.GetUserEntity(ssn);
        //        if (thjodskraPerson == null) return null;

        //        _dbContext.ThjodskraPersons.Add(thjodskraPerson);
        //        _dbContext.SaveChanges();
        //    }

        //    var user = await _userService.CreateUser(ssn, thjodskraPerson.Name, true);

        //    if (user == null)
        //    {
        //        return null;
        //    }

        //    var vmUser = new UserViewModel(user, false);

        //    return vmUser;
        //}

        [Route("delete")]
        [HttpPost]
        public void Delete([FromBody]string ssn)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == ssn);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
        }

    }
}
