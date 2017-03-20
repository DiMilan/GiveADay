using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.GroupViewModels;
using GoedBezigWebApp.Models.GroupState;
using GoedBezigWebApp.Models.Repositories;

namespace GoedBezigWebApp.Controllers
{
    public class HomeController : Controller
    {
        //added for localization
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;

        public HomeController(IStringLocalizer<HomeController> localizer, IUserRepository userRepository, IGroupRepository groupRepository)
        {
            _localizer = localizer;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }
        //end localization


        public IActionResult Index()
        {
            ViewData["MemberOfOrganization"] = false;
            ViewData["MemberOfGroup"] = false;
            ViewData["GroupApproved"] = false;
            ViewData["GroupSubmitted"] = false;
            ViewData["GBOrgAssigned"] = false;
            if (User.Identity.IsAuthenticated)
            {

                User user = _userRepository.GetBy(User.Identity.Name);
                _userRepository.LoadInvitations(user);
                if (user.Group != null)
                {

                    ViewData["GroupViewEditModel"] = new GroupEditViewModel(user.Group);
                    ViewData["MemberOfGroup"] = true;
                    if (user.Group.GroupState is MotivationSubmittedState)
                    {
                        ViewData["GroupSubmitted"] = true;
                    }
                    if (user.Group.GroupState is MotivationApprovedState)
                    {
                        ViewData["GroupSubmitted"] = true;
                        ViewData["GroupApproved"] = true;

                       
                    }
                    _groupRepository.LoadOrganizations(user.Group);
                    if (user.Group.ExternalOrganization != null)
                    {
                        ViewData["GBOrgAssigned"] = true;
                    }

                }
                if (user.Organization != null)
                {
                    ViewData["MemberOfOrganization"] = true;
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
