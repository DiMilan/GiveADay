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
            return RedirectToAction("Create", EditActivityEventViewModel.ActivityType.Activity);
        }

        public IActionResult CreateEvent()
        {
            return RedirectToAction("Create", EditActivityEventViewModel.ActivityType.Event);
        }

        public IActionResult Create(EditActivityEventViewModel.ActivityType type)
        {
            return View("Edit", new EditActivityEventViewModel(type));
        }

        public IActionResult Edit(int id)
        {
            var activity = _activityRepository.GetById(id);
            return View(new EditActivityEventViewModel(activity));
        }

        [HttpPost]
        public IActionResult Create(EditActivityEventViewModel model, User user)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "The form contains some errors";
                return View("Edit", model);
            }

            var group = GetGroup(user);

            if (model.Type == EditActivityEventViewModel.ActivityType.Activity)
            {

                var activity = new Activity
                {
                    Title = model.Title,
                    Description = model.Description
                };


                group.AddActivity(activity);
            }
            else
            {
                var @event = new Event
                {
                    Title = model.Title,
                    Description = model.Description,
                    Date = model.Date as DateTime? ?? DateTime.Today // Not really that pretty
                };

                group.AddActivity(@event);
            }

            _groupRepository.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(EditActivityEventViewModel model, User user)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "The form contains some errors";
                return View("Edit", model);
            }

            if (model.Id == null)
            {
                TempData["Error"] = "Trying to edit activity / event that does not exist";
                return RedirectToAction("Index");
            }

            if (model.Type == EditActivityEventViewModel.ActivityType.Activity)
            {
                var activity = GetActivity(model.Id.Value);

                activity.Title = model.Title;
                activity.Description = model.Description;

            }
            else
            {
                var @event = GetEvent(model.Id.Value);

                @event.Title = model.Title;
                @event.Description = model.Description;
                @event.Date = model.Date as DateTime? ?? DateTime.Today; // Not really that pretty
            }

            _activityRepository.SaveChanges();

            return RedirectToAction("Index");
        }

        private Group GetGroup(User user)
        {
            var group = user.Group;
            _groupRepository.LoadActivities(group);
            return group;
        }

        private Activity GetActivity(int id)
        {
            return _activityRepository.GetById(id);
        }

        private Event GetEvent(int id)
        {
            return GetActivity(id) as Event;
        }
    }
}