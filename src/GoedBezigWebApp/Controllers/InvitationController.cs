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


        public InvitationController(UserManager<User> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
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
                return View(user.GetPendingInvitations());
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
                var invitation = user.Invitations.FirstOrDefault(i => i.GroupId == id);

                if (invitation == null)
                {
                    TempData["error"] = string.Format("Invitation  does not exist (userId = {0}, groupId = {1})", user.Id, id);
                }
                else
                {
                    user.AcceptInvitation(invitation);
                    _userRepository.SaveChanges();
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
                var invitation = user.Invitations.FirstOrDefault(i => i.GroupId == id);

                if (invitation == null)
                {
                    TempData["error"] = string.Format("Invitation  does not exist (userId = {0}, groupId = {1})", user.Id, id);
                }
                else
                {
                    user.DeclineInvitation(invitation);
                    _userRepository.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        private async Task<User> GetCurrentUserAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            _userRepository.LoadInvitations(user);
            return user;
        }
    }
}