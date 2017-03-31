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
            var newUser = _userRepository.GetBy(name);

            if (newUser == null)
            {
                TempData["message"] = $"Trying to add user that does not exist!";
                return RedirectToAction("Index");
            }

            _groupRepository.LoadUsers(user.Group);

            if (newUser.Group != null)
            {
                if (user.Group.Equals(newUser.Group))
                {
                    TempData["error"] = $"User already member of this group!";
                }
                else
                {
                    TempData["error"] = $"User already member of another group!";

                }
                return RedirectToAction("Index");
            }

            if (user.Group.Invitations.Select(i => i.User).ToList().Contains(newUser))
            {
                TempData["error"] = $"User already invited to this group!";
                return RedirectToAction("Index");
            }

            try
            {
                user.Group.InviteUser(newUser);
                _groupRepository.SaveChanges();
                TempData["message"] = $"User successfully invited in group!";
            }
            catch (OrganizationException error)
            {
                TempData["error"] = error.Message;
            }

            return RedirectToAction("Index");
        }
    }
}