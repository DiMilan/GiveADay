using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Exceptions;
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

        public IActionResult Edit(string id)
        {
            Group group = _groupRepository.GetBy(id);
            return View(new GroupEditViewModel(group));
        }

        public IActionResult Create()
        {
            return View(nameof(Edit), new GroupEditViewModel(new Group()));
        }

        [HttpPost]
        public IActionResult Create(GroupEditViewModel groupEditViewModel)
        {
            string username="";

            if (ModelState.IsValid)
            {
                try
                {
                    if (User.Identity.IsAuthenticated)
                    {
                         username = User.Identity.Name;
                    }
                    Group group = new Group(groupEditViewModel);
                    _groupRepository.Add(group);
                    _groupRepository.SaveChanges();
                    TempData["message"] = $"{username} De groep {group.Name} werd succesvol aangemaakt.";
                    return View(nameof(Edit), groupEditViewModel);
                }
                catch (GroupExistsException)
                {
                    TempData["error"] = $"Er bestaat al een groep met de naam {groupEditViewModel.Name}";
                    groupEditViewModel.Name = null;
                    return View(nameof(Edit), groupEditViewModel);
                }
                catch (Exception)
                {
                    TempData["error"] = $"Er is iets fout gelopen. Groep {groupEditViewModel.Name} werd niet opgeslagen";
                }

            }
            TempData["error"] = "Er is een fout opgetreden";
            return View(nameof(Edit), groupEditViewModel);
        }

    }
}
