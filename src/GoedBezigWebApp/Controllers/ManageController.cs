using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.ManageViewModels;
using GoedBezigWebApp.Models.Repositories;
using GoedBezigWebApp.Models.UserViewModels;
using GoedBezigWebApp.Services;

namespace GoedBezigWebApp.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public IEmailSender EmailSender { get; }
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;

        public ManageController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IEmailSender emailSender,
        ISmsSender smsSender,
        ILoggerFactory loggerFactory,
        IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            EmailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<ManageController>();
            _userRepository = userRepository;
        }

        //
        // GET: /Manage/Index
        [HttpGet]
        [ServiceFilter(typeof(UserFilter))]
        public ViewResult Index(User user, ManageMessageId? message = null)
        {
            ViewData["User"] = new UserViewModel(user);
            if (user == null)
            {
                return View("Error");
            }
            UserViewModel userViewModel = new UserViewModel(user);
            return View("Index", userViewModel);
        }

        //
        //POST: /Manage/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(UserFilter))]
        public IActionResult UpdateProfile(UserViewModel userViewModel, User user)
        {
            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }
            if (user == null)
            {
                return View("Error");
            }
            ViewData["User"] = new UserViewModel(user);
            user.FirstName = userViewModel.FirstName;
            user.FamilyName = userViewModel.FamilyName;
            user.UserName = userViewModel.Username;
            _userRepository.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(UserFilter))]
        public async Task<IActionResult> RemoveLogin(RemoveLoginViewModel account, User user)
        {
            ViewData["User"] = new UserViewModel(user);
            ManageMessageId? message = ManageMessageId.Error;
            if (user != null)
            {
                var result = await _userManager.RemoveLoginAsync(user, account.LoginProvider, account.ProviderKey);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    message = ManageMessageId.RemoveLoginSuccess;
                }
            }
            return RedirectToAction(nameof(ManageLogins), new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        [ServiceFilter(typeof(UserFilter))]
        public IActionResult AddPhoneNumber(User user)
        {
            ViewData["User"] = new UserViewModel(user);
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(UserFilter))]
        public async Task<IActionResult> AddPhoneNumber(AddPhoneNumberViewModel model, User user)
        {
            ViewData["User"] = new UserViewModel(user);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            if (user == null)
            {
                return View("Error");
            }
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, model.PhoneNumber);
            await _smsSender.SendSmsAsync(model.PhoneNumber, "Your security code is: " + code);
            return RedirectToAction(nameof(VerifyPhoneNumber), new {model.PhoneNumber });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(UserFilter))]
        public async Task<IActionResult> EnableTwoFactorAuthentication(User user)
        {
            ViewData["User"] = new UserViewModel(user);
            if (user != null)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, true);
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation(1, "User enabled two-factor authentication.");
            }
            return RedirectToAction(nameof(Index), "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(UserFilter))]
        public async Task<IActionResult> DisableTwoFactorAuthentication(User user)
        {
            ViewData["User"] = new UserViewModel(user);
            if (user != null)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, false);
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation(2, "User disabled two-factor authentication.");
            }
            return RedirectToAction(nameof(Index), "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        [HttpGet]
        [ServiceFilter(typeof(UserFilter))]
        public async Task<IActionResult> VerifyPhoneNumber(string phoneNumber, User user)
        {
            ViewData["User"] = new UserViewModel(user);
            if (user == null)
            {
                return View("Error");
            }
            await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
            // Send an SMS to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(UserFilter))]
        public async Task<IActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model, User user)
        {
           ViewData["User"] = new UserViewModel(user);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (user != null)
            {
                var result = await _userManager.ChangePhoneNumberAsync(user, model.PhoneNumber, model.Code);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.AddPhoneSuccess });
                }
            }
            // If we got this far, something failed, redisplay the form
            ModelState.AddModelError(string.Empty, "Failed to verify phone number");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(UserFilter))]
        public async Task<IActionResult> RemovePhoneNumber(User user)
        {
            ViewData["User"] = new UserViewModel(user);
            if (user != null)
            {
                var result = await _userManager.SetPhoneNumberAsync(user, null);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.RemovePhoneSuccess });
                }
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }

        //
        // GET: /Manage/ChangePassword
        [HttpGet]
        [ServiceFilter(typeof(UserFilter))]
        public IActionResult ChangePassword(User user)
        {
            ViewData["User"] = new UserViewModel(user);
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(UserFilter))]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model, User user)
        {
            ViewData["User"] = new UserViewModel(user);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User changed their password successfully.");
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }

        //
        // GET: /Manage/SetPassword
        [HttpGet]
        [ServiceFilter(typeof(UserFilter))]
        public IActionResult SetPassword(User user)
        {
            ViewData["User"] = new UserViewModel(user);
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(UserFilter))]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model, User user)
        {
            ViewData["User"] = new UserViewModel(user);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (user != null)
            {
                var result = await _userManager.AddPasswordAsync(user, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }

        //GET: /Manage/ManageLogins
        [HttpGet]
        [ServiceFilter(typeof(UserFilter))]
        public async Task<IActionResult> ManageLogins(User user, ManageMessageId? message = null)
        {
            ViewData["User"] = new UserViewModel(user);
            ViewData["StatusMessage"] =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.AddLoginSuccess ? "The external login was added."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await _userManager.GetLoginsAsync(user);
            var otherLogins = _signInManager.GetExternalAuthenticationSchemes().Where(auth => userLogins.All(ul => auth.AuthenticationScheme != ul.LoginProvider)).ToList();
            ViewData["ShowRemoveButton"] = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(UserFilter))]
        public IActionResult LinkLogin(string provider, User user)
        {
            ViewData["User"] = new UserViewModel(user);
            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Action("LinkLoginCallback", "Manage");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User));
            return Challenge(properties, provider);
        }

        //
        // GET: /Manage/LinkLoginCallback
        [HttpGet]
        [ServiceFilter(typeof(UserFilter))]
        public async Task<ActionResult> LinkLoginCallback(User user)
        {
            
            if (user == null)
            {
                return View("Error");
            }
            ViewData["User"] = new UserViewModel(user);
            var info = await _signInManager.GetExternalLoginInfoAsync(await _userManager.GetUserIdAsync(user));
            if (info == null)
            {
                return RedirectToAction(nameof(ManageLogins), new { Message = ManageMessageId.Error });
            }
            var result = await _userManager.AddLoginAsync(user, info);
            var message = result.Succeeded ? ManageMessageId.AddLoginSuccess : ManageMessageId.Error;
            return RedirectToAction(nameof(ManageLogins), new { Message = message });
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            AddLoginSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        private Task<User> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        #endregion
    }
}
