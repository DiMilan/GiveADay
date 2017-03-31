using System.Linq;
using GoedBezigWebApp.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using GoedBezigWebApp.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
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

            ViewBag.Group = user.Group.GroupName;
            ViewBag.Org = user.Organization.Name;

            var users = _userRepository.GetAll()
                .Where(u => u.Organization != null)
                .Where(u2 => u2.Organization.Name == user.Organization.Name)
                .OrderBy(uf => uf.FamilyName)
                .ThenBy(uv => uv.FirstName);

            _groupRepository.LoadUsers(user.Group);

            var invitedUsers = user.Group.Invitations.Select(i => i.User);

            return View(users.Except(invitedUsers));
        }

        [ServiceFilter(typeof(UserFilter))]
        public IActionResult InviteUser(User user, String name)
        {
            var newUser = _userRepository.GetBy(name);

            if (newUser == null)
            {
                TempData["error"] = $"Trying to add user that does not exist!";
                return RedirectToAction("Index");
            }

            _groupRepository.LoadUsers(user.Group);

            if (newUser.Group != null)
            {
                if (user.Group.Equals(newUser.Group))
                {
                    TempData["error"] = newUser.FirstName + $" already member of this group!";
                }
                else
                {
                    TempData["error"] = newUser.FirstName + $" already member of another group!";

                }
                return RedirectToAction("Index");
            }

            if (user.Group.Invitations.Select(i => i.User).ToList().Contains(newUser))
            {
                TempData["error"] = newUser.FirstName + $" already invited to this group!";
                return RedirectToAction("Index");
            }

            try
            {
                user.Group.InviteUser(newUser);
                _groupRepository.SaveChanges();
                TempData["message"] = newUser.FirstName + $" successfully invited in group!";
            }
            catch (OrganizationException error)
            {
                TempData["error"] = error.Message;
            }

            return RedirectToAction("Index");
        }
    }
}