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
            if (user.Group != null) { ViewBag.Group = user.Group.GroupName; }
            return View(_userRepository.GetAll().OrderBy(u => u.FamilyName).ThenBy(u2 => u2.FirstName));
        }

        [ServiceFilter(typeof(UserFilter))]
        public IActionResult InviteUser(String name, User user)
        {
            ViewData["User"] = new UserViewModel(user);
            // check geeft NullReferenceException op if(_userRepository.GetBy(name).Group == null)
            // nog uit te zoeken

            //if (_userRepository.GetBy(name).Group.Equals(null))
            //{
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

            //}
            //else if (_userRepository.GetBy(name).Group.GroupName == user.Group.GroupName)
            //    {
            //    TempData["message"] = $"User already invited in this group!";
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    TempData["message"] = $"User already invited in another group!";
            //    return RedirectToAction("Index");
            //}
        }
    }
}