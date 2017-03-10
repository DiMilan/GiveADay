using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.GroupViewModels;
using GoedBezigWebApp.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace GoedBezigWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository, IStringLocalizer<HomeController> localizer)
        {
            _userRepository = userRepository;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            return View(_userRepository.GetAll().OrderBy(u =>u.FamilyName).ThenBy(u2 =>u2.FirstName));


        }
    }
}
