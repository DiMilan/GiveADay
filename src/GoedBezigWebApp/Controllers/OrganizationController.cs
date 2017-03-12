using System;
using System.Threading.Tasks;
using Castle.Core.Internal;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Exceptions;
using GoedBezigWebApp.Models.Repositories;
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

        public OrganizationController(UserManager<User> userManager, IOrganizationRepository organizationRepository, IUserRepository userRepository)
        {
            _userManager = userManager;
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index(String searchName, String searchLocation)
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return View("Error");
            }

            ViewData["searchName"] = searchName;
            ViewData["searchLocation"] = searchLocation;
            ViewBag.Cities = _organizationRepository.GetAllUniqueCities();
            return View(_organizationRepository.GetAllFilteredByNameAndLocation(searchName,searchLocation));
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
                Organization organization = _organizationRepository.GetBy(id);
                User userToEdit = _userRepository.GetBy(user.UserName);
                if(!user.Email.Split('@')[1].Contains(organization.Domain)) throw new OrganizationException("Your email address has to have the extension of the organization you want to be in.");
                userToEdit.Organization = organization;
                _userRepository.SaveChanges();
                TempData["message"] =
                    $"{user.FirstName}, you have been added to the organization {organization.Name} succesfully.";
                return RedirectToAction("Index");
            }
            catch (OrganizationException error)
            {
                TempData["error"] = error.Message;
                return RedirectToAction("Index");
            }
            
            
        }

        private Task<User> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
