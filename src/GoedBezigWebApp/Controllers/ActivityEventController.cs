using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.ActivityEventViewModels;
using GoedBezigWebApp.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoedBezigWebApp.Controllers
{
    [Authorize]
    public class ActivityEventController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public ActivityEventController(IGroupRepository groupRepository, IUserRepository userRepository, UserManager<User> userManager)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            _userRepository.LoadInvitations(user);

            if (user.Group == null)
            {
                TempData["Error"] = "You are not part of a group yet";
                return RedirectToAction("Index", "Home");
            }

            _groupRepository.LoadActivities(user.Group);

            var activities = user.Group.GetActivities();
            var events = user.Group.GetEvents();

            return View(new ActivityEventViewModel(activities, events));
        }
        private async Task<User> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }
    }
}