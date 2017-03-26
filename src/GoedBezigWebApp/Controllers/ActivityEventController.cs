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
        private readonly IActivityRepository _activityRepository;

        public ActivityEventController(IGroupRepository groupRepository, IActivityRepository activityRepository)
        {
            _groupRepository = groupRepository;
            _activityRepository = activityRepository;
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

        public IActionResult CreateActivity()
        {
            return View("Edit", new EditActivityEventViewModel(EditActivityEventViewModel.ActivityType.Activity));
        }

        public IActionResult CreateEvent()
        {
            return View("Edit", new EditActivityEventViewModel(EditActivityEventViewModel.ActivityType.Event));
        }

        public IActionResult Edit(int id)
        {
                var activity = _activityRepository.GetById(id);
                return View(new EditActivityEventViewModel(activity));

        }
    }
}