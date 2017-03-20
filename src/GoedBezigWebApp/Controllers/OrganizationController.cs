using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Castle.Core.Internal;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Exceptions;
using GoedBezigWebApp.Models.Repositories;
using GoedBezigWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

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

        public async Task<IActionResult> Index(String searchName, String searchLocation, bool isExternalWithLabel, bool isExternalWithoutLabel, string groupId)
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return View("Error");
            }

            ViewData["searchName"] = searchName;
            ViewData["searchLocation"] = searchLocation;
            
            ViewBag.User = user;
            if (groupId.IsNullOrEmpty() && !isExternalWithLabel && !isExternalWithoutLabel)
            {
                //Load only gb-orgs
                ViewData["title"] = "GB Organizations";
                ViewBag.Cities = _organizationRepository.GetAllGbUniqueCities();
                return View(_organizationRepository.GetAllGbFilteredByNameAndLocation(searchName, searchLocation));
            }

            if (groupId.IsNullOrEmpty() && isExternalWithLabel && !isExternalWithoutLabel)
            {
                //Load only external orgs with Label
                ViewData["title"] = "External Organizations with GBLabel";
                ViewBag.isExternalWithLabel = true;
                ViewBag.Cities = _organizationRepository.GetAllExternalWithLabelUniqueCities();
                return View(_organizationRepository.GetAllExternalWithLabelFilteredByNameAndLocation(searchName, searchLocation));
            }

            if (groupId.IsNullOrEmpty() && !isExternalWithLabel && isExternalWithoutLabel)
            {
                //Load only external orgs without label
                ViewData["title"] = "External Organizations without GBLabel";
                ViewBag.isExternalWithoutLabel = true;
                ViewBag.Cities = _organizationRepository.GetAllExternalWithoutLabelUniqueCities();
                return View(_organizationRepository.GetAllExternalWithoutLabelFilteredByNameAndLocation(searchName, searchLocation));
            }

            else if (!groupId.IsNullOrEmpty() && checkEntitledToGiveGBLabel(groupId))
            {
                //Load only external orgs without label - ready to give label
                ViewData["title"] = "Give GBLabel to External Organization";
                ViewBag.Group = _groupRepository.GetBy(groupId);
                ViewBag.Cities = _organizationRepository.GetAllExternalWithoutLabelUniqueCities();
                return View(_organizationRepository.GetAllExternalWithoutLabelFilteredByNameAndLocation(searchName, searchLocation));
            }
            else
            {
                TempData["error"] =
                    "The request is not valid or the GroupId is either not valid or the group is not entitled to give a GBLabel to an organization.";
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
                return RedirectToAction("Index","Home");
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

        public async Task<IActionResult> AssignGBLabel(int id, string groupId)
        {
            var user = await GetCurrentUserAsync();

            if (user == null || id == 0 || groupId.IsNullOrEmpty() || !checkEntitledToGiveGBLabel(groupId))
            {
                return View("Error");
            }



            ViewBag.Group = _groupRepository.GetBy(groupId);
            return View(_organizationRepository.GetExternalOrganizationBy(id));
        }

        [HttpPost]
        public async Task<IActionResult> AssignGBLabel(int id, string groupId, List<ContactRecord> notifyContacts)
        {
            var user = await GetCurrentUserAsync();
            

            if (user == null || id == 0 || groupId.IsNullOrEmpty() || !checkEntitledToGiveGBLabel(groupId))
            {
                TempData["error"] =
                    "The request is not valid or the GroupId is either not valid or the group is not entitled to give a GBLabel to an organization.";
                return View("Error");
            }

            try
            {
                _organizationRepository.GetExternalOrganizationBy(id).AssignGbLabel(_groupRepository.GetBy(groupId),notifyContacts);
                _organizationRepository.SaveChanges();
                TempData["message"] = "The organization has succesfully been given a GB label, the selected stakeholders have been contacted.";
            }

            catch (OrganizationException error)
            {
                TempData["error"] = error.Message;
                return RedirectToAction("AssignGBLabel", new { id = id, groupId = groupId });
            }

            return RedirectToAction("Edit","Group", new { id = groupId });
        }

        private bool checkEntitledToGiveGBLabel(string groupId)
        {
            var group = _groupRepository.GetBy(groupId);
            _groupRepository.LoadOrganizations(group);
            _groupRepository.LoadUsers(group);
            return group.EntitledToGiveGbLabel();
        }
    }
}
