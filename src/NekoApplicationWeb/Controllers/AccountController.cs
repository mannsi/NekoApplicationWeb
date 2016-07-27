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
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IThjodskraService _thjodskraService;

        public AccountController(ILogger<AccountController> logger, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService,
            ApplicationDbContext applicationDbContext,
            IHttpContextAccessor httpContextAccessor,
            IThjodskraService thjodskraService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _applicationDbContext = applicationDbContext;
            _httpContextAccessor = httpContextAccessor;
            _thjodskraService = thjodskraService;
        }

        [Route("")]
        public async Task<IActionResult> StartPage()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("StartPage");
            }
            var vm = new StartPageViewModel();
            return View(vm);
        }

        [Route("NyUmsokn")]
        [HttpGet]
        public IActionResult CreateApplication()
        {
            var vm = new CreateApplicationViewModel();
            return View(vm);
        }

        [Route("NyUmsokn")]
        [HttpPost]
        public async Task<IActionResult> CreateApplication(CreateApplicationViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var ssn = vm.Ssn.Replace("-", "");

                // Check validity of ssn
                if (!ssn.IsValidSsn())
                {
                    _logger.LogWarning($"Invalid ssn when creating application. Ssn used '{vm.Ssn}', email used '{vm.Email}'");
                    ModelState.AddModelError("", "Grunsamleg kennitala");
                    return View(vm);
                }

                // Check if user already exists
                var user = await _userManager.FindByNameAsync(ssn);
                if (user != null)
                {
                    _logger.LogWarning($"Previously used ssn when creating application. Ssn used '{vm.Ssn}', email used '{vm.Email}'");
                    ModelState.AddModelError("", "Kennitala er þegar til");
                    return View(vm);
                }

                user = new ApplicationUser
                {
                    UserName = ssn,
                    //UserDisplayName = ssn,
                    Email = vm.Email
                };

                var createResult = await _userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                {
                    _logger.LogWarning($"Unable to create user when creating application. Error message: {createResult.Errors}. Ssn used '{vm.Ssn}', email used '{vm.Email}'");
                    ModelState.AddModelError("", "Ekki tókst að stofna notanda");
                    return View(vm);
                }

                var passwordResult = await _userManager.AddPasswordAsync(user, vm.Password);
                if (!passwordResult.Succeeded)
                {
                    _logger.LogWarning($"Unable to set password when creating application. Error message: {createResult.Errors}. Ssn used '{vm.Ssn}', email used '{vm.Email}'");
                    ModelState.AddModelError("", "Ekki tókst að setja lykilorð fyrir notanda");
                    return View(vm);
                }

                await SendConfirmationEmail(user, Url, HttpContext);
                return View("EmailConfirmationSent");
            }

            return View(vm);
        }

        [Route("StadfestaReikning")]
        public async Task<IActionResult> ConfirmEmailAndSetPassword(string userId, string code)
        {
            var loggedInUser = _applicationDbContext.Users.FirstOrDefault(user => user.Id == _userManager.GetUserId(_httpContextAccessor.HttpContext.User)); ;
            if (loggedInUser != null)
            {
                await _signInManager.SignOutAsync(); 
                return RedirectToAction("ConfirmEmailAndSetPassword", new { userId = userId, code = code });
            }

            if (userId == null || code == null)
            {
                _logger.LogWarning($"Trying to confirm user but either user Id is null or his code is null. UserId: '{userId}', code: '{code}'");
                return View("Error");
            }
            var confirmingUser = await _userManager.FindByIdAsync(userId);
            if (confirmingUser == null)
            {
                _logger.LogWarning($"Trying to confirm user but user object not found for userId: '{userId}'");
                return View("Error");
            }

            var confirmEmailResult = await _userManager.ConfirmEmailAsync(confirmingUser, code);

            if (confirmEmailResult.Succeeded)
            {
                await _signInManager.SignInAsync(confirmingUser, false);
                return RedirectToAction("Index", "Page");
            }

            return View("Error", "Ekki tókst að staðfesta notanda");
        }

        [Route("GleymtLykilord")]
        public IActionResult ForgotPassword()
        {
            return View("StartPage");
        }

        [HttpPost]
        [Route("Innskra")]
        public async Task<IActionResult> Login(StartPageViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Innskráning tókst ekki");
                return View("StartPage");
            }

            var user = await _userManager.FindByNameAsync(vm.Ssn.Replace("-", ""));
            if (user == null)
            {
                ModelState.AddModelError("", "Innskráning tókst ekki");
                return View("StartPage");
            }

            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError("", "Getur verið að þú eigir eftir að staðfesta reikningin þinn. Tölvupóstur með leiðbeiningum ætti að hafa borist.");
                return View("StartPage");
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);
            if (signInResult.Succeeded)
            {
                //if (string.IsNullOrEmpty(user.UserDisplayName))
                //{
                //    var userEntity = _thjodskraService.GetUserEntity(vm.Ssn.Replace("-", ""));
                //    var familyList = _thjodskraService.UserFamilyInfo(userEntity.FamilyNumber);

                //    if (userEntity != null)
                //    {
                //        user.UserDisplayName = userEntity.Name;
                //    }

                //    await _userManager.UpdateAsync(user);
                //}

                return RedirectToAction("Index", "Page");
            }
            else
            {
                ModelState.AddModelError("", "Innskráning tókst ekki");
                return View("StartPage");
            }
        }

        [HttpPost]
        [Route("Utskra")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("StartPage");
        }

        private async Task SendConfirmationEmail(ApplicationUser user, IUrlHelper url, HttpContext httpContext)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = url.Action(nameof(AccountController.ConfirmEmailAndSetPassword), "Account", new { userId = user.Id, code = code }, protocol: httpContext.Request.Scheme);

            var emailBody = $"Þetta netfang var notað til að skrá umsókn um Neko fasteignalán. Smelltu á <a href=\"{callbackUrl}\">hérna</a> til að staðfesta reikningin þinn.";

            _emailService.SendEmailAsync(user.Email, "Neko umsókn", emailBody);
        }

        private async Task IslandLogin(string ssn)
        {
            var user = await _userManager.FindByNameAsync(ssn);
            if (user == null)
            {
                // This is a new user. Redirect him to the EULA page
            }

            //if (!user.HasAggreedToEula)
            {
                // Show user EULA page
            }
            
        }
        
    }
}
