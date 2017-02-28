using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.GroupViewModels;
using GoedBezigWebApp.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Edit(string name)
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

            if (ModelState.IsValid)
            {
                try
                {
                    Group group = new Group();
                    MapGroupEditViewModelToGroup(groupEditViewModel, group);
                    group.Timestamp = DateTime.Now;
                    _groupRepository.Add(group);
                    _groupRepository.SaveChanges();
                    TempData["message"] = $"De groep {group.Name} werd succesvol aangemaakt.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    TempData["error"] = "Er bestaat al een groep met deze naam";
                    return RedirectToAction(nameof(Edit), groupEditViewModel);
                }
                catch (Exception)
                {
                    TempData["error"] = $"Er is iets fout gelopen. Groep {groupEditViewModel.Name} werd niet opgeslagen";
                }

            }
            TempData["error"] = "Er is een fout opgetreden";
            return RedirectToAction(nameof(Edit), groupEditViewModel);
        }


        private void MapGroupEditViewModelToGroup(GroupEditViewModel groupEditViewModel, Group group)
        {
            group.Name = groupEditViewModel.Name;
            group.Timestamp = groupEditViewModel.Timestamp;
            group.ClosedGroup = groupEditViewModel.ClosedGroup;
        }
    }
}
