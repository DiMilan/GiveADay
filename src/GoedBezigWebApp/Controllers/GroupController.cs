using System;
using System.Threading.Tasks;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Exceptions;
using GoedBezigWebApp.Models.GroupViewModels;
using GoedBezigWebApp.Models.Repositories;
using GoedBezigWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using GoedBezigWebApp.Models.MotivationState;

namespace GoedBezigWebApp.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public GroupController(UserManager<User> userManager, IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                TempData["error"] = "User not logged in";
                return View();
            }
            else
            {
                //filter enkel de groepen:
                // van de organisatie waar de ingelogde user deel van uitmaakt => CHECK
                // die niet gesloten zijn => CHECK
                // met een motivatie die nog niet goegekeurd is 
                    return View(_groupRepository.GetAll()
                        .Where(
                        (g => g.GBOrganization == user.Organization 
                        && g.ClosedGroup == false 
                        && !(g.MotivationStatus is ApprovedState))//wat is de status voor GOEDGEKEURD ?  
                    ));
            }
        }

        public IActionResult Edit(string id)
        {
            Group group = _groupRepository.GetBy(id);
            return View(new GroupEditViewModel(group));
        }

        [HttpPost]
        public IActionResult Edit(GroupEditViewModel groupEditViewModel)
        {
            if (ModelState.IsValid)
            {
                Group group = null;
                try
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        string username = User.Identity.Name;
                        User user = _userRepository.GetBy(username);
                        group = _groupRepository.GetBy(groupEditViewModel.Name);              
                        group.MotivationStatus.SaveMotivation(groupEditViewModel.Motivation);
                        group.MotivationStatus.AddCompanyDetails(groupEditViewModel.CompanyName, groupEditViewModel.CompanyAddress, groupEditViewModel.CompanyEmail, groupEditViewModel.CompanyWebsite);
                        group.MotivationStatus.AddCompanyContact(groupEditViewModel.CompanyContactName, groupEditViewModel.CompanyContactSurname, groupEditViewModel.CompanyContactEmail, groupEditViewModel.CompanyContactTitle);
                        _groupRepository.SaveChanges();
                        TempData["message"] = $"{username} De groep {group.GroupName} werd succesvol aangepast.";
                        return View(nameof(Index));
                    }
                }
                catch (GroupExistsException)
                {
                    TempData["error"] = $"Er bestaat al een groep met de naam {groupEditViewModel.Name}. Kies een andere naam";
                    groupEditViewModel.Name = null;
                }
                catch (MotivationException e)
                {

                    TempData["error"] = e.Message.ToString();
                }
                //catch (Exception)
                //{
                //    TempData["error"] = $"Er is iets fout gelopen. Groep {groupEditViewModel.Name} werd niet opgeslagen";
                //    groupEditViewModel.Name = null;
                //}

            }
            return View(nameof(Edit), groupEditViewModel);
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
                        group.Users.Add(user);
                        group.MotivationStatus.AddCompanyDetails(groupEditViewModel.CompanyName, groupEditViewModel.CompanyAddress, groupEditViewModel.CompanyEmail, groupEditViewModel.CompanyWebsite);
                        group.MotivationStatus.AddCompanyContact(groupEditViewModel.CompanyContactName, groupEditViewModel.CompanyContactSurname, groupEditViewModel.CompanyContactEmail, groupEditViewModel.CompanyContactTitle);
                        group.MotivationStatus.SaveMotivation(groupEditViewModel.Motivation);
                        _groupRepository.SaveChanges();

                        TempData["message"] = $"{username} De groep {group.GroupName} werd succesvol aangemaakt. U kan de groep nog bewerken om zaken aan te passen.";

                        //Mail notification to lector
                        //@Bart: waar vind ik de lector die bij het aanmaken van een groep onderstaande mail moet krijgen?

                        var mailer = new AuthMessageSender();
                        var sendMail = mailer.SendEmailAsync("bartjevm@gmail.com",
                            "Group has been added",
                            String.Format("Hi Lector,\n\na group has been added to the GiveADay Platform called {0} has been created.\n\nKind regards,\nGiveADay Bot", group.GroupName),
                            String.Format("<p>Hi Lector,<p><p>a group has been added to the GiveADay Platform called {0} has been created.</p><p>Kind regards<br>GiveADay Bot</p>", group.GroupName));
                        return View(nameof(Index));
                    }
                }
                catch (GroupExistsException)
                {
                    TempData["error"] = $"Er bestaat al een groep met de naam {groupEditViewModel.Name}. Kies een andere naam";
                    groupEditViewModel.Name = null;
                }
                catch (MotivationException e)
                {

                    TempData["error"] = e.Message.ToString();
                }
                //catch (Exception)
                //{
                //    TempData["error"] = $"Er is iets fout gelopen. Groep {groupEditViewModel.Name} werd niet opgeslagen";
                //    groupEditViewModel.Name = null;
                //}

            }
            return View(nameof(Edit), groupEditViewModel);
        }
        public IActionResult SubmitMotivation(string id)
        {
            ViewData[nameof(Group.GroupName)] = _groupRepository.GetBy(id).GroupName;
            return View();
        }

        [HttpPost, ActionName("SubmitMotivation")]
        public IActionResult SubmitConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        Group group = _groupRepository.GetBy(id);
                        group.MotivationStatus.SubmitMotivation();
                        _groupRepository.SaveChanges();
                        TempData["message"] = $"De motivatie van {group.GroupName} werd succesvol doorgestuurd.";

                        //Mail notification to lector
                        //@Bart: waar vind ik de lector die bij het aanmaken van een groep onderstaande mail moet krijgen?

                        var mailer = new AuthMessageSender();
                        var sendMail = mailer.SendEmailAsync("bartjevm@gmail.com",
                            "Motivation has been submitted",
                            String.Format(
                                "Hi Lector,\n\na motivation has been added to group {0} of the GiveADay Platform.\n\nKind regards,\nGiveADay Bot",
                                group.GroupName),
                            String.Format(
                                "Hi Lector,\n\na motivation has been added to group {0} of the GiveADay Platform.\n\nKind regards,\nGiveADay Bot",
                                group.GroupName));
                        return View(nameof(Index));
                    }
                }
                catch (MotivationException e)
                {

                    TempData["error"] = $"De motivatie werd niet ingediend. {e.Message.ToString()}";
                }
                //catch (Exception)
                //{
                //    TempData["error"] = $"Er is iets fout gelopen. Groep {groupEditViewModel.Name} werd niet opgeslagen";
                //    groupEditViewModel.Name = null;
                //}

            }
            return View(nameof(Index));
        }
        private async Task<User> GetCurrentUserAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return user != null ? _userRepository.GetBy(user.UserName) : null;
        }
        public async Task<IActionResult> AddUser(String id)
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                TempData["error"] = "User not logged in";
                return View("Error");
            }
            else
            {

                try
                {
                    _groupRepository.GetBy(id).AddUser(user);
                    _groupRepository.SaveChanges();
                    TempData["message"] = $"User successfully added to group!";
                    return RedirectToAction("Index");

                }
                catch (OrganizationException error)
                {
                    TempData["error"] = error.Message;
                    return RedirectToAction("Index");
                }
            }
        }
    }
}
