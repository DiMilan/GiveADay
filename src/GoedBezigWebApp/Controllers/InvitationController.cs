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
        private readonly IUserRepository _userRepository;
        private readonly IInvitationRepository _invitationRepository;


        public InvitationController(UserManager<User> userManager, IUserRepository userRepository, IInvitationRepository invitationRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _invitationRepository = invitationRepository;
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return View("Error");
            }
            
            return View(_invitationRepository.GetForUser(user));
        }
        private Task<User> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}