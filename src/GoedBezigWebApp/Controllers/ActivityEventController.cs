using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Filters;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.ActivityEventViewModels;
using GoedBezigWebApp.Models.Repositories;
using GoedBezigWebApp.Models.UserViewModels;
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
        [ServiceFilter(typeof(UserFilter))]
        public IActionResult Index(User user)
        {
            ViewData["User"] = new UserViewModel(user);
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
    }
}