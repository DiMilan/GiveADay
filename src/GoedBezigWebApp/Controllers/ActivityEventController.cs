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
            return View("EditActivity", new EditActivityViewModel());
        }

        public IActionResult CreateEvent()
        {
            return View("EditEvent", new EditEventViewModel());
        }

        [HttpPost]
        public IActionResult CreateActivity(EditActivityViewModel model, User user)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "The form contains some errors";
                return View("EditActivity", model);
            }

            var activity = new Activity
            {
                Title = model.Title,
                Description = model.Description
            };

            return Create(activity, user);
        }

        [HttpPost]
        public IActionResult CreateEvent(EditEventViewModel model, User user)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "The form contains some errors";
                return View("EditEvent", model);
            }

            var @event = new Event
            {
                Title = model.Title,
                Description = model.Description,
                Date = model.Date ?? DateTime.Today
            };

            return Create(@event, user);
        }

        private IActionResult Create(Activity activity, User user)
        {
            var group = GetGroup(user);

            group.AddActivity(activity);

            _groupRepository.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult EditActivity(int id)
        {
            var activity = GetActivity(id);
            return View(new EditActivityViewModel(activity));
        }

        public IActionResult EditEvent(int id)
        {
            var @event = GetEvent(id);
            return View(new EditEventViewModel(@event));
        }

        [HttpPost]
        public IActionResult EditActivity(EditActivityViewModel model, User user)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "The form contains some errors";
                return View("EditActivity", model);
            }

            if (model.Id == null)
            {
                TempData["Error"] = "Trying to edit activity / event that does not exist";
                return RedirectToAction("Index");
            }

            var activity = GetActivity(model.Id.Value);

            activity.Title = model.Title;
            activity.Description = model.Description;

            _activityRepository.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditEvent(EditEventViewModel model, User user)
        {

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "The form contains some errors";
                return View("EditActivity", model);
            }

            if (model.Id == null)
            {
                TempData["Error"] = "Trying to edit activity / event that does not exist";
                return RedirectToAction("Index");
            }

            var @event = GetEvent(model.Id.Value);

            @event.Title = model.Title;
            @event.Description = model.Description;
            @event.Date = model.Date ?? DateTime.Today;

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