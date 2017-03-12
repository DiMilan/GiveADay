using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoedBezigWebApp.Controllers
{
    [Authorize]
    public class InvitationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IInvitationRepository _invitationRepository;


        public InvitationController(UserManager<User> userManager, IInvitationRepository invitationRepository)
        {
            _userManager = userManager;
            _invitationRepository = invitationRepository;
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
                return View(_invitationRepository.GetForUser(user));
            }
        }

        public async Task<IActionResult> Accept(string id)
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                TempData["error"] = "User not logged in";
            }
            else
            {
                var invitation = _invitationRepository.GetById(user.Id, id);

                if (invitation == null)
                {
                    TempData["error"] = string.Format("Invitation  does not exist (userId = {0}, groupId = {1})", user.Id, id);
                }
                else
                {
                    invitation.Accept();
                    _invitationRepository.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Decline(string id)
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                TempData["error"] = "User not logged in";
            }
            else
            {
                var invitation = _invitationRepository.GetById(user.Id, id);

                if (invitation == null)
                {
                    TempData["error"] = string.Format("Invitation  does not exist (userId = {0}, groupId = {1})", user.Id, id);
                }
                else
                {
                    invitation.Decline();
                    _invitationRepository.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        private Task<User> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}