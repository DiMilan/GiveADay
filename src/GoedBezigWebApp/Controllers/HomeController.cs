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

        public IActionResult About()
        {
            ViewData["Message"] = _localizer["Your application description page."];
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = _localizer["Your contact page."];
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
