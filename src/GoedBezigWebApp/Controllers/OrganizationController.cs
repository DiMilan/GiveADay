using System;
using System.Threading.Tasks;
using Castle.Core.Internal;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Exceptions;
using GoedBezigWebApp.Models.Repositories;
using GoedBezigWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NUglify.Helpers;

namespace GoedBezigWebApp.Controllers
{
    [Authorize]
    public class OrganizationController : Controller
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IGroupRepository _groupRepository;

        public OrganizationController(UserManager<User> userManager, IOrganizationRepository organizationRepository,
            IUserRepository userRepository, IGroupRepository groupRepository)
        {
            _userManager = userManager;
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }

        public async Task<IActionResult> Index(String searchName, String searchLocation, string groupId)
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return View("Error");
            }

            ViewData["searchName"] = searchName;
            ViewData["searchLocation"] = searchLocation;
            
            ViewBag.User = user;
            if (groupId.IsNullOrEmpty())
            {
                //Load only gb-label orgs
                ViewBag.Cities = _organizationRepository.GetAllGbUniqueCities();
                return View(_organizationRepository.GetAllGbFilteredByNameAndLocation(searchName, searchLocation));
            }
            else if (_groupRepository.GetBy(groupId).entitledToGiveGBLabel())
            {
                //Load only not-GB-Label orgs - not implemented yet
                ViewBag.Group = _groupRepository.GetBy(groupId);
                ViewBag.Cities = _organizationRepository.GetAllExternalOrganizationsWithoutLabel();
                return View(_organizationRepository.GetAllExternalWithoutLabelFilteredByNameAndLocation(searchName, searchLocation));
            }
            else
            {
                TempData["error"] =
                    "The GroupId is either not valid or not entitled to give a GBLabel to an organization.";
                return View("Error");
            }

        }

        public async Task<IActionResult> Register(int id = 0)
        {
            var user = await GetCurrentUserAsync();

            if (user == null || id == 0)
            {
                return View("Error");
            }

            try
            {
                _userRepository.GetBy(user.UserName).RegisterInOrganization(_organizationRepository.GetGbOrganizationBy(id));
                _userRepository.SaveChanges();
                TempData["message"] = $"You have been added to the organization succesfully!";
                return RedirectToAction("Index");

            }
            catch (OrganizationException error)
            {
                TempData["error"] = error.Message;
                return RedirectToAction("Index");
            }


        }

        private async Task<User> GetCurrentUserAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            _userRepository.LoadGbOrganization(user);
            return user;
        }

        public async Task<IActionResult> AssignLabel(string id)
        {
            //to be implemented
            TempData["message"] = $"TO BE DONE";

            //Move to OrganizationModel

            var mailer = new AuthMessageSender();
            var sendMail = mailer.SendEmailAsync("devloomax@mdware.org",
                            "Organization X got the GB Label",
                            String.Format("XXX\nXXX"),
                            String.Format("XXX<br>XXX"));

            return RedirectToAction("Index");
        }
    }
}
