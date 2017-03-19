using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.ActivityEventViewModels;
using GoedBezigWebApp.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoedBezigWebApp.Controllers
{
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

        public IActionResult Index()
        {
            var activities = new List<Activity>()
            {
                new Activity() { Title = "Activity 1", Accepted = true },
                new Activity() { Title = "Activity 2", Accepted = true },
                new Activity() { Title = "Activity 3", Accepted = false },

            };

            var events = new List<Event>()
            {
                new Event() { Title = "Event 1", Date = DateTime.Now.AddDays(2), Accepted = true },
                new Event() { Title = "Event 4", Date = DateTime.Now.AddDays(35), Accepted = true },
                new Event() { Title = "Event 3", Date = DateTime.Now.AddDays(25), Accepted = false },
                new Event() { Title = "Event 2", Date = DateTime.Now.AddDays(12), Accepted = false }
            };

            return View(new ActivityEventViewModel(activities, events));
        }
    }
}