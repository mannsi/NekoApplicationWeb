using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ServiceInterfaces;
using NekoApplicationWeb.ViewModels.Account;
using static NekoApplicationWeb.Shared.ExtensionMethods;

namespace NekoApplicationWeb.Controllers
{
    [Route("")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IThjodskraService _thjodskraService;

        public AccountController(ILogger<AccountController> logger, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService,
            ApplicationDbContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            IThjodskraService thjodskraService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _thjodskraService = thjodskraService;
        }

        //[Route("")]
        //public async Task<IActionResult> StartPage()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        await _signInManager.SignOutAsync();
        //        return RedirectToAction("StartPage");
        //    }
        //    var vm = new StartPageViewModel();
        //    return View(vm);
        //}

        //[Route("NyUmsokn")]
        //[HttpGet]
        //public IActionResult CreateApplication()
        //{
        //    var vm = new CreateApplicationViewModel();
        //    return View(vm);
        //}

        //[Route("NyUmsokn")]
        //[HttpPost]
        //public async Task<IActionResult> CreateApplication(CreateApplicationViewModel vm)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var ssn = vm.Ssn.Replace("-", "");

        //        // Check validity of ssn
        //        if (!ssn.IsValidSsn())
        //        {
        //            _logger.LogWarning($"Invalid ssn when creating application. Ssn used '{vm.Ssn}', email used '{vm.Email}'");
        //            ModelState.AddModelError("", "Grunsamleg kennitala");
        //            return View(vm);
        //        }

        //        // Check if user already exists
        //        var user = await _userManager.FindByNameAsync(ssn);
        //        if (user != null)
        //        {
        //            _logger.LogWarning($"Previously used ssn when creating application. Ssn used '{vm.Ssn}', email used '{vm.Email}'");
        //            ModelState.AddModelError("", "Kennitala er þegar til");
        //            return View(vm);
        //        }

        //        user = new ApplicationUser
        //        {
        //            UserName = ssn,
        //            //UserDisplayName = ssn,
        //            Email = vm.Email
        //        };

        //        var createResult = await _userManager.CreateAsync(user);
        //        if (!createResult.Succeeded)
        //        {
        //            _logger.LogWarning($"Unable to create user when creating application. Error message: {createResult.Errors}. Ssn used '{vm.Ssn}', email used '{vm.Email}'");
        //            ModelState.AddModelError("", "Ekki tókst að stofna notanda");
        //            return View(vm);
        //        }

        //        var passwordResult = await _userManager.AddPasswordAsync(user, vm.Password);
        //        if (!passwordResult.Succeeded)
        //        {
        //            _logger.LogWarning($"Unable to set password when creating application. Error message: {createResult.Errors}. Ssn used '{vm.Ssn}', email used '{vm.Email}'");
        //            ModelState.AddModelError("", "Ekki tókst að setja lykilorð fyrir notanda");
        //            return View(vm);
        //        }

        //        await SendConfirmationEmail(user, Url, HttpContext);
        //        return View("EmailConfirmationSent");
        //    }

        //    return View(vm);
        //}

        //[Route("StadfestaReikning")]
        //public async Task<IActionResult> ConfirmEmailAndSetPassword(string userId, string code)
        //{
        //    var loggedInUser = _applicationDbContext.Users.FirstOrDefault(user => user.Id == _userManager.GetUserId(_httpContextAccessor.HttpContext.User)); ;
        //    if (loggedInUser != null)
        //    {
        //        await _signInManager.SignOutAsync(); 
        //        return RedirectToAction("ConfirmEmailAndSetPassword", new { userId = userId, code = code });
        //    }

        //    if (userId == null || code == null)
        //    {
        //        _logger.LogWarning($"Trying to confirm user but either user Id is null or his code is null. UserId: '{userId}', code: '{code}'");
        //        return View("Error");
        //    }
        //    var confirmingUser = await _userManager.FindByIdAsync(userId);
        //    if (confirmingUser == null)
        //    {
        //        _logger.LogWarning($"Trying to confirm user but user object not found for userId: '{userId}'");
        //        return View("Error");
        //    }

        //    var confirmEmailResult = await _userManager.ConfirmEmailAsync(confirmingUser, code);

        //    if (confirmEmailResult.Succeeded)
        //    {
        //        await _signInManager.SignInAsync(confirmingUser, false);
        //        return RedirectToAction("Index", "Page");
        //    }

        //    return View("Error", "Ekki tókst að staðfesta notanda");
        //}

        //[Route("GleymtLykilord")]
        //public IActionResult ForgotPassword()
        //{
        //    return View("StartPage");
        //}

        //[HttpPost]
        //[Route("Innskra")]
        //public async Task<IActionResult> Login(StartPageViewModel vm)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ModelState.AddModelError("", "Innskráning tókst ekki");
        //        return View("StartPage");
        //    }

        //    var user = await _userManager.FindByNameAsync(vm.Ssn.Replace("-", ""));
        //    if (user == null)
        //    {
        //        ModelState.AddModelError("", "Innskráning tókst ekki");
        //        return View("StartPage");
        //    }

        //    if (!user.EmailConfirmed)
        //    {
        //        ModelState.AddModelError("", "Getur verið að þú eigir eftir að staðfesta reikningin þinn. Tölvupóstur með leiðbeiningum ætti að hafa borist.");
        //        return View("StartPage");
        //    }

        //    var signInResult = await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);
        //    if (signInResult.Succeeded)
        //    {
        //        //if (string.IsNullOrEmpty(user.UserDisplayName))
        //        //{
        //        //    var userEntity = _thjodskraService.GetUserEntity(vm.Ssn.Replace("-", ""));
        //        //    var familyList = _thjodskraService.UserFamilyInfo(userEntity.FamilyNumber);

        //        //    if (userEntity != null)
        //        //    {
        //        //        user.UserDisplayName = userEntity.Name;
        //        //    }

        //        //    await _userManager.UpdateAsync(user);
        //        //}

        //        return RedirectToAction("Index", "Page");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Innskráning tókst ekki");
        //        return View("StartPage");
        //    }
        //}

        [HttpPost]
        [Route("Utskra")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("StartPage");
        }

        [HttpGet]
        [Route("")]
        public IActionResult FakeSignIn()
        {
            return View();
        }

        [HttpPost]
        [Route("FakeSignIn")]
        public async Task<IActionResult> FakeSignIn(string ssn)
        {
            // Only the user that created the application can sign in. Other applicants can only verify via Island.is
            var user = await _userManager.FindByIdAsync(ssn);
            if (user == null)
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    user = await CreateNewApplication(ssn);
                    transaction.Commit();
                }
                await _signInManager.SignInAsync(user, true);
            }
            else
            {
                bool creatingUserLoggingIn = _dbContext.Applications.Any(appl => appl.CreatedByUser == user);

                if (creatingUserLoggingIn)
                {
                    await _signInManager.SignInAsync(user, true);
                }
                else
                {
                    // The user logging in is an extra user that is being verified
                    return RedirectToAction(nameof(PageController.Personal), "Page", new {verifyingUser = user});

                }
            }

            return RedirectToAction(nameof(PageController.Start), "Page");
        }

        private async Task<ApplicationUser> CreateNewApplication(string ssn)
        {
            var thjodskraPerson = FetchAndSaveThjodskraData(ssn);
            var user = new ApplicationUser
            {
                Id = ssn,
                UserName = thjodskraPerson.Name,
                IsDeletable = false
            };

            await _userManager.CreateAsync(user);

            ApplicationUser spouseUser = null;

            if (!string.IsNullOrEmpty(thjodskraPerson.SpouseSsn))
            {
                var spouseThjodskraPerson = FetchAndSaveThjodskraData(thjodskraPerson.SpouseSsn);

                // Create applicant from spouse info
                spouseUser = new ApplicationUser
                {
                    Id = thjodskraPerson.SpouseSsn,
                    UserName = spouseThjodskraPerson.Name,
                    IsDeletable = false
                };

                await _userManager.CreateAsync(spouseUser);
            }

            FetchAndSaveCreditInfoData(ssn);

            var application = new Application
            {
                CreatedByUser = user,
                TimeCreated = DateTime.Now
            };

            _dbContext.Applications.Add(application);

            var applicationUserConnection = new ApplicationUserConnection
            {
                Application = application,
                User = user,
                UserHasAgreedToEula = false
            };

            _dbContext.ApplicationUserConnections.Add(applicationUserConnection);

            if (spouseUser != null)
            {
                var applicationSpouseUserConnection = new ApplicationUserConnection
                {
                    Application = application,
                    User = spouseUser,
                    UserHasAgreedToEula = false
                };

                _dbContext.ApplicationUserConnections.Add(applicationSpouseUserConnection);
            }
            _dbContext.SaveChanges();

            return user;
        } 

        private void FetchAndSaveCreditInfoData(string ssn)
        {
            // TODO
        }

        private ThjodskraPerson FetchAndSaveThjodskraData(string ssn)
        {
            var thjodskraPerson = _thjodskraService.GetUserEntity(ssn);
            if (thjodskraPerson == null)
            {
                throw new Exception("No thjodskra entry found for ssn");
            }

            thjodskraPerson.TimeOfData = DateTime.Now;
            _dbContext.ThjodskraPersons.Add(thjodskraPerson);

            var thjodskraFamilyEntries = _thjodskraService.UserFamilyInfo(thjodskraPerson.FamilyNumber);
            if (thjodskraFamilyEntries != null)
            {
                foreach (var thjodskraFamilyEntry in thjodskraFamilyEntries)
                {
                    thjodskraFamilyEntry.TimeOfData = DateTime.Now;
                    _dbContext.ThjodskraFamilyEntries.Add(thjodskraFamilyEntry);
                }
            }

            _dbContext.SaveChanges();

            return thjodskraPerson;
        }
               
    }
}
