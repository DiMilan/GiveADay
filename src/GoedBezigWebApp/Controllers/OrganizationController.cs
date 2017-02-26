using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.GroupViewModels;
using GoedBezigWebApp.Models.OrganizationViewModels;
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

        public IActionResult Index()
        {
            return View(_organizationRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Index(OrganizationSearchViewModel organizationSearchModel)
        {
            return View(_organizationRepository.GetAll());
        }
    }
}
