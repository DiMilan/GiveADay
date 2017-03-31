using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Filters;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Repositories;
using GoedBezigWebApp.Models.UserViewModels;
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
        [ServiceFilter(typeof(UserFilter))]
        public IActionResult Index(User user)
        {
            ViewData["User"] = new UserViewModel(user);

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
        [ServiceFilter(typeof(UserFilter))]
        public IActionResult Accept(string id, User user)
        {
            ViewData["User"] = new UserViewModel(user);
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

        [ServiceFilter(typeof(UserFilter))]
        public IActionResult Decline(string id, User user)
        {
            ViewData["User"] = new UserViewModel(user);
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

    }
}