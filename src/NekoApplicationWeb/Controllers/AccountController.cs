using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using CreditScore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.ServiceInterfaces;
using NekoApplicationWeb.ViewModels.Account;
using static NekoApplicationWeb.Shared.ExtensionMethods;
using ILogger = Serilog.ILogger;

namespace NekoApplicationWeb.Controllers
{
    [Route("")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IThjodskraService _thjodskraService;
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService,
            ApplicationDbContext dbContext,
            IHttpContextAccessor httpContextAccessor,
            IThjodskraService thjodskraService,
            IUserService userService,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _thjodskraService = thjodskraService;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [Route("Utskra")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(AccountController.MyFakeSignIn), "Account");
        }

        [HttpGet]
        [Route("")]
        public IActionResult MyFakeSignIn()
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
            }

            return RedirectToAction(nameof(PageController.Personal), "Page", new { verifyingUserId = user.Id });
        }

        private async Task<ApplicationUser> CreateNewApplication(string ssn)
        {
            var thjodskraPerson = FetchAndSaveThjodskraData(ssn);
            var user = await _userService.CreateUser(ssn, thjodskraPerson.Name, false);

            ApplicationUser spouseUser = null;

            if (!string.IsNullOrEmpty(thjodskraPerson.SpouseSsn))
            {
                var spouseThjodskraPerson = FetchAndSaveThjodskraData(thjodskraPerson.SpouseSsn);

                // Create applicant from spouse info
                spouseUser = await _userService.CreateUser(thjodskraPerson.SpouseSsn, spouseThjodskraPerson.Name, false);
            }

            FetchAndSaveCreditInfoData(ssn);

            var application = new Application
            {
                CreatedByUser = user,
                TimeCreated = DateTime.Now
            };

            _dbContext.Applications.Add(application);

            var emptyPropertyDetailsForApplication = new PropertyDetail {Application = application, BuyingPrice = 0, OwnCapital = 0, PropertyNumber = ""};
            _dbContext.PropertyDetails.Add(emptyPropertyDetailsForApplication);

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
            var thjodskraPerson = _dbContext.ThjodskraPersons.FirstOrDefault(per => per.Id == ssn);
            if (thjodskraPerson != null)
            {
                return thjodskraPerson;
            }

            thjodskraPerson = _thjodskraService.GetUserEntity(ssn);
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
