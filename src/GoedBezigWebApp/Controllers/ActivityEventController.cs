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
            var activities = new List<Activity>();

            var events = new List<Event>();

            return View(new ActivityEventViewModel(activities, events));
        }
    }
}