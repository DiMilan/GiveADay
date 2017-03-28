using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Filters;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

            return View(tasks);
        }
    }
}