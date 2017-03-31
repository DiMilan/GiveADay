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
            ViewData["Users"] = GetUsersSelectList(user, activityTask);
            ViewData["Activities"] = GetActivitiesSelectList(user, activityTask);
            ViewData["States"] = GetCurrentStateSelectList(user, activityTask);
            return View(new ActivityTaskEditViewModel(activityTask));
        }

       

        public IActionResult Create(User user)
        {
            ViewData["Users"] = GetUsersSelectList(user, null);
            ViewData["Activities"] = GetActivitiesSelectList(user, null);
            ViewData["States"] = GetCurrentStateSelectList(user, null);
            return View(nameof(Edit), new ActivityTaskEditViewModel());
        }

        private SelectList GetActivitiesSelectList(User user, ActivityTask activityTask)
        {
            return new SelectList(user.Group.Activities.OrderBy(b => b.Title), nameof(Models.Activity.Id), nameof(Models.Activity.Title), activityTask?.Activity?.Id);
        }

        private SelectList GetCurrentStateSelectList(User user, ActivityTask activityTask)
        {
            return new SelectList(Enum.GetValues(typeof(TaskState)).Cast<TaskState>().ToList(),activityTask?.CurrentState);
        }

        private MultiSelectList GetUsersSelectList(User user, ActivityTask activityTask)
        {
            return new MultiSelectList(user.Group.Users.OrderBy(b => b.UserName), nameof(Models.User.UserName), nameof(Models.User.UserName), activityTask?.Users?.Select(u => u.UserName));
        }
    }
}