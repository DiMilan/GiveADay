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
using GoedBezigWebApp.Filters;
using GoedBezigWebApp.Models.GroupState;

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
        [ServiceFilter(typeof(UserFilter))]
        public IActionResult Index(User user)
        {
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
                    (g => g.GbOrganization == user.Organization
                    && g.ClosedGroup == false
                    && !(g.GroupState is MotivationApprovedState))//wat is de status voor GOEDGEKEURD ?  
                ));
            }
        }
        [ServiceFilter(typeof(UserFilter))]
        public IActionResult Edit(string id, User user)
        {
            Group group = _groupRepository.GetBy(id);
            _groupRepository.LoadOrganizations(group);
            ViewBag.ExternalOrganization = group.ExternalOrganization;
            return View(new GroupEditViewModel(group));
        }

        [HttpPost]
        [ServiceFilter(typeof(UserFilter))]
        public IActionResult Edit(GroupEditViewModel groupEditViewModel, User user)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var group = _groupRepository.GetBy(groupEditViewModel.Name);
                    group.SaveMotivation(groupEditViewModel.Motivation);
                    group.GroupState.AddCompanyDetails(groupEditViewModel.CompanyName, groupEditViewModel.CompanyAddress, groupEditViewModel.CompanyEmail, groupEditViewModel.CompanyWebsite);
                    group.GroupState.AddCompanyContact(groupEditViewModel.CompanyContactName, groupEditViewModel.CompanyContactSurname, groupEditViewModel.CompanyContactEmail, groupEditViewModel.CompanyContactTitle);
                    _groupRepository.SaveChanges();
                    TempData["message"] = $"{user.UserName} De groep {group.GroupName} werd succesvol aangepast.";
                    return RedirectToAction(nameof(Index), "Home");

                }
                catch (GroupExistsException)
                {
                    TempData["error"] = $"Er bestaat al een groep met de naam {groupEditViewModel.Name}. Kies een andere naam";
                    groupEditViewModel.Name = null;
                }
                catch (MotivationException e)
                {

                    TempData["error"] = e.Message;
                }
                //catch (Exception)
                //{
                //    TempData["error"] = $"Er is iets fout gelopen. Groep {groupEditViewModel.Name} werd niet opgeslagen";
                //    groupEditViewModel.Name = null;
                //}

            }
            return View(nameof(Edit), groupEditViewModel);
        }

        [ServiceFilter(typeof(UserFilter))]
        public IActionResult Create(User user)
        {
            return View(nameof(Edit), new GroupEditViewModel(new Group()));
        }

        [HttpPost]
        [ServiceFilter(typeof(UserFilter))]

        public async Task<IActionResult> Create(GroupEditViewModel groupEditViewModel, User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_groupRepository.Present(groupEditViewModel.Name)) { throw new GroupExistsException(); }

                    Group group = user.Organization.AddGroup(groupEditViewModel.Name, user);
                    group.SaveMotivation(groupEditViewModel.Motivation);
                    group.AddCompanyDetails(groupEditViewModel.CompanyName, groupEditViewModel.CompanyAddress, groupEditViewModel.CompanyEmail, groupEditViewModel.CompanyWebsite);
                    group.AddCompanyContact(groupEditViewModel.CompanyContactName, groupEditViewModel.CompanyContactSurname, groupEditViewModel.CompanyContactEmail, groupEditViewModel.CompanyContactTitle);
                    _groupRepository.SaveChanges();
                    TempData["message"] = $"{user.UserName} De groep {group.GroupName} werd succesvol aangemaakt. U kan de groep nog bewerken om zaken aan te passen.";

                    //Mail notification to lector
                    //@Bart: waar vind ik de lector die bij het aanmaken van een groep onderstaande mail moet krijgen?

                    var mailer = new AuthMessageSender();
                    await mailer.SendEmailAsync("bartjevm@gmail.com",
                        "Group has been added",
                        String.Format("Hi Lector,\n\na group has been added to the GiveADay Platform called {0} has been created.\n\nKind regards,\nGiveADay Bot", group.GroupName),
                        String.Format("<p>Hi Lector,<p><p>a group has been added to the GiveADay Platform called {0} has been created.</p><p>Kind regards<br>GiveADay Bot</p>", group.GroupName));
                    return RedirectToAction(nameof(Index), "Home");

                }
                catch (GroupExistsException)
                {
                    TempData["error"] = $"Er bestaat al een groep met de naam {groupEditViewModel.Name}. Kies een andere naam";
                    groupEditViewModel.Name = null;
                }
                catch (MotivationException e)
                {

                    TempData["error"] = e.Message;
                }
                //catch (Exception)
                //{
                //    TempData["error"] = $"Er is iets fout gelopen. Groep {groupEditViewModel.Name} werd niet opgeslagen";
                //    groupEditViewModel.Name = null;
                //}

            }
            return View(nameof(Edit), groupEditViewModel);
        }
        [ServiceFilter(typeof(UserFilter))]
        public IActionResult SubmitMotivation(string id, User user)
        {
            ViewData[nameof(Group.GroupName)] = _groupRepository.GetBy(id).GroupName;
            return View();
        }

        [HttpPost, ActionName("SubmitMotivation")]
        [ServiceFilter(typeof(UserFilter))]
        public async Task<IActionResult> SubmitConfirmed(string id, User user)
        {
            GroupEditViewModel groupEditViewModel = null;
            Group group = null;

            if (ModelState.IsValid)
            {


                try
                {

                    group = _groupRepository.GetBy(id);
                    group.GroupState.SubmitMotivation();
                    _groupRepository.SaveChanges();
                    TempData["message"] = $"De motivatie van {group.GroupName} werd succesvol doorgestuurd.";

                    //Mail notification to lector
                    //@Bart: waar vind ik de lector die bij het aanmaken van een groep onderstaande mail moet krijgen?

                    var mailer = new AuthMessageSender();
                    await mailer.SendEmailAsync("bartjevm@gmail.com",
                        "Motivation has been submitted",
                        String.Format(
                            "Hi Lector,\n\na motivation has been added to group {0} of the GiveADay Platform.\n\nKind regards,\nGiveADay Bot",
                            group.GroupName),
                        String.Format(
                            "Hi Lector,<br><br>a motivation has been added to group {0} of the GiveADay Platform.<br><br>Kind regards,<br>GiveADay Bot",
                            group.GroupName));
                    return RedirectToAction("Index", "Home");

                }
                catch (MotivationException e)
                {

                    TempData["error"] = $"De motivatie werd niet ingediend. {e.Message}";
                    groupEditViewModel = new GroupEditViewModel(group);
                }
                //catch (Exception)
                //{
                //    TempData["error"] = $"Er is iets fout gelopen. Groep {groupEditViewModel.Name} werd niet opgeslagen";
                //    groupEditViewModel.Name = null;
                //}

            }
            return View(nameof(Edit), groupEditViewModel);
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
                    group.GroupState = new MotivationApprovedState(group);
                    _groupRepository.SaveChanges();
                    TempData["message"] = $"De motivatie van {group.GroupName} werd op Approved gezet.";
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
