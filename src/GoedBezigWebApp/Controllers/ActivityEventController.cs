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
    [ServiceFilter(typeof(UserFilter))]
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
        public IActionResult Index(User user)
        {
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

        public IActionResult Edit(User user, int id)
        {
            return View(new EditActivityEventViewModel());
        }
    }
}