using System;
using System.Threading.Tasks;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Exceptions;
using GoedBezigWebApp.Models.GroupViewModels;
using GoedBezigWebApp.Models.Repositories;
using GoedBezigWebApp.Services;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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
                        TempData["message"] = $"{username} De groep {group.GroupName} werd succesvol aangemaakt.";

                        //Mail notification to lector
                        //@Bart: waar vind ik de lector die bij het aanmaken van een groep onderstaande mail moet krijgen?
                        var mailer = new AuthMessageSender();
                        var sendMail =  mailer.SendEmailAsync("devloomax@mdware.org",
                            "Group has been added",
                            String.Format("Hi Lector,\n\na group has been added to the GiveADay Platform called {0} has been created.\n\nKind regards,\nGiveADay Bot", group.GroupName),
                            String.Format("<p>Hi Lector,<p><p>a group has been added to the GiveADay Platform called {0} has been created.</p><p>Kind regards<br>GiveADay Bot</p>", group.GroupName));
                        return View(nameof(Edit), groupEditViewModel);
                    }
                }
                catch (GroupExistsException)
                {
                    TempData["error"] = $"Er bestaat al een groep met de naam {groupEditViewModel.Name}. Kies een andere naam";
                    groupEditViewModel.Name = null;
                }
                //catch (Exception)
                //{
                //    TempData["error"] = $"Er is iets fout gelopen. Groep {groupEditViewModel.Name} werd niet opgeslagen";
                //    groupEditViewModel.Name = null;
                //}

            }
            return View(nameof(Edit), groupEditViewModel);
        }

    }
}
