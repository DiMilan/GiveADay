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
using Microsoft.EntityFrameworkCore;

namespace GoedBezigWebApp.Controllers
{
    [Authorize]
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
            _groupRepository.LoadOrganizations(group);
            ViewBag.ExternalOrganization = group.ExternalOrganization;
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
                        return RedirectToAction(nameof(Index), "Home");
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
                    if(_groupRepository.Present(groupEditViewModel.Name)){ throw new GroupExistsException();}
                    if (User.Identity.IsAuthenticated)
                    {
                        string username = User.Identity.Name;
                        User user = _userRepository.GetBy(username);
                        Group group = user.Organization.AddGroup(groupEditViewModel.Name);
                        user.Invitations.Add(new Invitation(user, group, InvitationStatus.Accepted));
                        group.MotivationStatus.AddCompanyDetails(groupEditViewModel.CompanyName, groupEditViewModel.CompanyAddress, groupEditViewModel.CompanyEmail, groupEditViewModel.CompanyWebsite);
                        group.MotivationStatus.AddCompanyContact(groupEditViewModel.CompanyContactName, groupEditViewModel.CompanyContactSurname, groupEditViewModel.CompanyContactEmail, groupEditViewModel.CompanyContactTitle);
                        group.MotivationStatus.SaveMotivation(groupEditViewModel.Motivation);
                        _groupRepository.SaveChanges();
                        //group.GBOrganization = null;//op de een of andere manier zet EF de GB-Organisatie op 1 bij oproepen SaveChanges()...
                        //_groupRepository.SaveChanges();
                        TempData["message"] = $"{username} De groep {group.GroupName} werd succesvol aangemaakt. U kan de groep nog bewerken om zaken aan te passen.";

                        //Mail notification to lector
                        //@Bart: waar vind ik de lector die bij het aanmaken van een groep onderstaande mail moet krijgen?

                        var mailer = new AuthMessageSender();
                        var sendMail = mailer.SendEmailAsync("bartjevm@gmail.com",
                            "Group has been added",
                            String.Format("Hi Lector,\n\na group has been added to the GiveADay Platform called {0} has been created.\n\nKind regards,\nGiveADay Bot", group.GroupName),
                            String.Format("<p>Hi Lector,<p><p>a group has been added to the GiveADay Platform called {0} has been created.</p><p>Kind regards<br>GiveADay Bot</p>", group.GroupName));
                        return RedirectToAction(nameof(Index), "Home");
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
                                "Hi Lector,<br><br>a motivation has been added to group {0} of the GiveADay Platform.<br><br>Kind regards,<br>GiveADay Bot",
                                group.GroupName));
                        return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Index", "Home");
        }
        private async Task<User> GetCurrentUserAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return user != null ? _userRepository.GetBy(user.UserName) : null;
        }
        public async Task<IActionResult> AddUser(String id)
        {
            var user = await GetCurrentUserAsync();
            _userRepository.LoadInvitations(user);

            if (user == null)
            {
                TempData["error"] = "User not logged in";
                return View("Error");
            }
            else if (user.Group != null)
                {
                    TempData["error"] = "User already member of a group";
                    return RedirectToAction("Index", "Home");
            }
            else
            {

                try
                {
                    _groupRepository.GetBy(id).AddUser(user);
                     

            _groupRepository.SaveChanges();
                    TempData["message"] = $"User successfully added to group!";
                    return RedirectToAction("Index", "Home");

                }
                catch (OrganizationException error)
                {
                    TempData["error"] = error.Message;
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public IActionResult Approve(string id)
        {
            ViewData[nameof(Group.GroupName)] = _groupRepository.GetBy(id).GroupName;
            return View();
        }

        [HttpPost, ActionName("Approve")]
        public IActionResult ApproveConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                
                    if (User.Identity.IsAuthenticated)
                    {
                        Group group = _groupRepository.GetBy(id);
                        group.MotivationStatus = new ApprovedState(group);
                        _groupRepository.SaveChanges();
                        TempData["message"] = $"De motivatie van {group.GroupName} werd op Approved gezet.";
                        return RedirectToAction("Index", "Home");
                    }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
