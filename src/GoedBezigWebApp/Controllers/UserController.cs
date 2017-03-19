﻿using System.Linq;
using GoedBezigWebApp.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using GoedBezigWebApp.Models;
using System.Threading.Tasks;
using System;
using GoedBezigWebApp.Models.Exceptions;

namespace GoedBezigWebApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly UserManager<User> _userManager;

        public UserController(IUserRepository userRepository, UserManager<User> userManager, IGroupRepository groupRepository, IStringLocalizer<HomeController> localizer)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _localizer = localizer;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return View("Error");
            }
            ViewBag.User = user;
            if (user.Group != null) { ViewBag.Group = user.Group.GroupName; }
            return View(_userRepository.GetAll().OrderBy(u => u.FamilyName).ThenBy(u2 => u2.FirstName));


        }

        private async Task<User> GetCurrentUserAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            _userRepository.LoadInvitations(user);
            return user;
        }
        public async Task<IActionResult> InviteUser(String name)
        {
            var user = await GetCurrentUserAsync();
            _userRepository.LoadInvitations(user);
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
    }


}