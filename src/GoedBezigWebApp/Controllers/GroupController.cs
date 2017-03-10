using System;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Exceptions;
using GoedBezigWebApp.Models.GroupViewModels;
using GoedBezigWebApp.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GoedBezigWebApp.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;

        public GroupController(IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
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
            if (ModelState.IsValid)
            {
                try
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        string username = User.Identity.Name;
                        User user = _userRepository.GetBy(username);
                        Group group = user.Organization.AddGroup(groupEditViewModel.Name);
                        _groupRepository.Add(group);
                        _groupRepository.SaveChanges();
                        TempData["message"] = $"{username} De groep {group.Name} werd succesvol aangemaakt.";
                        return View(nameof(Edit), groupEditViewModel);
                    }
                }
                catch (GroupExistsException)
                {
                    TempData["error"] = $"Er bestaat al een groep met de naam {groupEditViewModel.Name}. Kies een andere naam";
                    groupEditViewModel.Name = null;
                }
                catch (Exception)
                {
                    TempData["error"] = $"Er is iets fout gelopen. Groep {groupEditViewModel.Name} werd niet opgeslagen";
                    groupEditViewModel.Name = null;
                }

            }
            return View(nameof(Edit), groupEditViewModel);
        }

    }
}
