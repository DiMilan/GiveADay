using System.Linq;
using GoedBezigWebApp.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using GoedBezigWebApp.Models;
using System.Threading.Tasks;

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

            ViewBag.User = user.Group;
            return View(_userRepository.GetAll().OrderBy(u =>u.FamilyName).ThenBy(u2 =>u2.FirstName));


        }

        private async Task<User> GetCurrentUserAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            _userRepository.LoadGroups(user);
            return user;
        }


    }
}
