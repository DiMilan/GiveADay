using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.GroupViewModels;
using GoedBezigWebApp.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GoedBezigWebApp.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupRepository _groupRepository;

        public GroupController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            Group group = new Group();
            return View(new GroupEditViewModel(group));
        }

        public IActionResult Create()
        {
            return View(nameof(Edit), new GroupEditViewModel(new Group()));
        }

        [HttpPost]
        public IActionResult Create(GroupEditViewModel groupEditViewModel)
        {
            Group group = new Group();
            try
            {
                MapGroupEditViewModelToGroup(groupEditViewModel, group);
                _groupRepository.Add(group);
                _groupRepository.SaveChanges();
                TempData["message"] = $"You successfully added group {group.Name}.";
            }
            catch
            {
                TempData["error"] = "Sorry, something went wrong, the group was not created...";
            }
            return RedirectToAction(nameof(Index));
        }

        private void MapGroupEditViewModelToGroup(GroupEditViewModel groupEditViewModel, Group group)
        {
            group.Name = groupEditViewModel.Name;
            group.Timestamp = groupEditViewModel.Timestamp;
            group.ClosedGroup = groupEditViewModel.ClosedGroup;
        }
    }
}
