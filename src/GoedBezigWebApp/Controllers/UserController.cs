using System.Linq;
using GoedBezigWebApp.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using GoedBezigWebApp.Models;
using System.Threading.Tasks;
using System;
using GoedBezigWebApp.Filters;
using GoedBezigWebApp.Models.Exceptions;
using GoedBezigWebApp.Models.UserViewModels;

namespace GoedBezigWebApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        public UserController(IUserRepository userRepository, IGroupRepository groupRepository, IStringLocalizer<HomeController> localizer)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _localizer = localizer;
        }

        [ServiceFilter(typeof(UserFilter))]
        public IActionResult Index(User user)
        {
            ViewData["User"] = new UserViewModel(user);
            if (user == null)
            {
                return View("Error");
            }
            if (user.Group != null) {
                ViewBag.Group = user.Group.GroupName;
                ViewBag.Org = user.Organization.Name;
            }
            return View(_userRepository.GetAll()
                
                .Where(u => u.Organization != null)
                .Where(u2 => u2.Organization.Name == user.Organization.Name)
                .OrderBy(uf => uf.FamilyName)
                .ThenBy(uv => uv.FirstName));
        }

        [ServiceFilter(typeof(UserFilter))]
        public IActionResult InviteUser(User user, String name)
        {
            ViewData["User"] = new UserViewModel(user);
            _groupRepository.LoadUsers(user.Group);
            // check om te zien of de user nog niet in een groep is ingeschreven
            if (_userRepository.GetBy(name) != null 
                && _userRepository.GetBy(name).Group == null
                //deze check lijkt niet te werken
                && !user.Group.Invitations.Select(i => i.User).ToList().Contains(_userRepository.GetBy(name))
                )
            {
                try
                {
                    _groupRepository.GetBy(user.Group.GroupName).InviteUser(_userRepository.GetBy(name));
                    //_groupRepository.SaveChanges();
                    TempData["message"] = $"User successfully invited in group!";
                    return RedirectToAction("Index");

                }
                catch (OrganizationException error)
                {
                    TempData["error"] = error.Message;
                    return RedirectToAction("Index");
                }

            }
            else if (_userRepository.GetBy(name).Group.GroupName == user.Group.GroupName 
                ||_groupRepository.GetBy(user.Group.GroupName).Users.Contains(_userRepository.GetBy(name))
                )
            {
                TempData["error"] = $"User already member or invited in this group!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = $"User already member of another group!";
                return RedirectToAction("Index");
            }
        }
    }
}