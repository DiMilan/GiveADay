using GoedBezigWebApp.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace GoedBezigWebApp.Controllers
{
    public class HomeController : Controller
    {
        //added for localization
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(IStringLocalizer<HomeController> localizer)
        {
            _localizer = localizer;
        }
        //end localization


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
