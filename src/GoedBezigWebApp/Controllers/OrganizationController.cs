using System;
using GoedBezigWebApp.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GoedBezigWebApp.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationController(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public IActionResult Index(String searchName, String searchLocation)
        {
            ViewData["searchName"] = searchName;
            ViewData["searchLocation"] = searchLocation;
            ViewBag.Cities = _organizationRepository.GetAllUniqueCities();
            return View(_organizationRepository.GetAllFilteredByNameAndLocation(searchName,searchLocation));
        }
    }
}
