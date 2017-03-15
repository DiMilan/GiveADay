﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Controllers;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Repositories;
using GoedBezigWebApp.Tests.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;

namespace GoedBezigWebApp.Tests.Controllers
{
    public class OrganizationControllerTest
    {
        private readonly OrganizationController _controller;
        private readonly Mock<IOrganizationRepository> _organizationRepository;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<UserManager<User>> _userManager;
        private readonly DummyGoedBezigDbContext _dummyContext;

        public OrganizationControllerTest()
        {
            _dummyContext = new DummyGoedBezigDbContext();
            _organizationRepository = new Mock<IOrganizationRepository>();
            _controller = new OrganizationController(_userManager.Object,_organizationRepository.Object, _userRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
        }
    }
}