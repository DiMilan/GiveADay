using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Repositories;

namespace GoedBezigWebApp.Controllers
{
    public class HomeController : Controller
    {
        //added for localization
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IUserRepository _userRepository;

        public HomeController(IStringLocalizer<HomeController> localizer, IUserRepository userRepository)
        {
            _localizer = localizer;
            _userRepository = userRepository;
        }
        //end localization


        public IActionResult Index()
        {
            ViewData["MemberOfOrganization"] = false;
            ViewData["MemberOfGroup"] = false;
            if (User.Identity.IsAuthenticated)
            {
                User user = _userRepository.GetBy(User.Identity.Name);
                if (user.Organization!=null)
                {
                    ViewData["MemberOfOrganization"] = true;
                } else if (user.Group != null)
                {
                        ViewData["MemberOfGroup"] = true;
                }

            }
            return View();
        }
        //added for localization language choice handler
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
