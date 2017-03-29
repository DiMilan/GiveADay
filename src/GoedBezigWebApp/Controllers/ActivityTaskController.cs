using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Filters;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.ActivityTaskViewModels;
using GoedBezigWebApp.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GoedBezigWebApp.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(UserFilter))]
    public class ActivityTaskController : Controller
    {
        private readonly IActivityTaskRepository _activityTaskRepository;

        public ActivityTaskController(IActivityTaskRepository activityTaskRepository)
        {
            _activityTaskRepository = activityTaskRepository;
        }

        //GET ActivityTask/Index
        public IActionResult Index(User user)
        {
            if (user == null)
            {
                return View("Error");
            }
            IEnumerable<ActivityTask> tasks = _activityTaskRepository.GetAll().OrderBy(d => d.FromDateTime);
                //.Where(a => user.Group.Activities.Contains(a.Activity));
            ICollection<ActivityTaskEditViewModel> activityTaskEditViewModels = new List<ActivityTaskEditViewModel>();
            foreach (ActivityTask at in tasks)
            {
                activityTaskEditViewModels.Add(new ActivityTaskEditViewModel(at));

            }
            return View(activityTaskEditViewModels);
        }

        public IActionResult Edit(int id, User user)
        {
            ActivityTask activityTask = _activityTaskRepository.GetBy(id);
            ViewData["Users"] = new MultiSelectList(user.Group.Users, nameof(Models.User.UserName), nameof(Models.User.UserName));
            return View(new ActivityTaskEditViewModel(activityTask));
        }

        public IActionResult Create(User user)
        {
            return View(nameof(Edit), new ActivityTaskEditViewModel());
        }
    }
}