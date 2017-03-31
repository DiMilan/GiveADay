using System;
using System.Security.Principal;
using GoedBezigWebApp.Controllers;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.GroupViewModels;
using GoedBezigWebApp.Models.Repositories;
using GoedBezigWebApp.Tests.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace GoedBezigWebApp.Tests.Controllers
{
    public class GroupControllerTest
    {
        private readonly GroupController _controller;
        private readonly Mock<IGroupRepository> _groupRepository;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly DummyGoedBezigDbContext _dummyContext;
        private User testUser { get; set; }
        public GroupControllerTest()
        {
            
            _dummyContext = new DummyGoedBezigDbContext();
            _groupRepository = new Mock<IGroupRepository>();
            _userRepository = new Mock<IUserRepository>();
            _controller = new GroupController(null, _groupRepository.Object, _userRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object,
                ControllerContext = new ControllerContext {HttpContext = new DefaultHttpContext()}
            };
            var testUsername = new GenericIdentity("testUser");
            _controller.ControllerContext.HttpContext.User = new GenericPrincipal(testUsername, null);
            testUser= new User()
            {
                FirstName = "test",
                FamilyName = "test",
                Email = "test@test.be",
                NormalizedEmail = "test@test.be".ToUpper(),
                UserName = "test",
                NormalizedUserName = "test".ToUpper(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                LockoutEnabled = true,
            };
        }

        #region -- Create GET --
        [Fact]
        public void CreateMustPassNewGroupInEditViewModel()
        {
            IActionResult action = _controller.Create(testUser);
            GroupEditViewModel groupEditViewModelEvm = (action as ViewResult)?.Model as GroupEditViewModel;
            Assert.Equal(null, groupEditViewModelEvm?.Name);
        }
        #endregion
        #region -- Create POST --
        [Fact]
        public async void CreateRedirectsToActionIndexWhenSuccessfull()
        {
            testUser.Organization = new GbOrganization();
            _userRepository.Setup(p => p.GetBy("testUser")).Returns(testUser);
            GroupEditViewModel groupEditViewModel = new GroupEditViewModel(new Group()
            {
                GroupName = "Project123",
                ClosedGroup = true,
                Timestamp = new DateTime(2017,02,15,18,52,45)
                        
            });
            RedirectToActionResult action = await _controller.Create(groupEditViewModel, testUser) as RedirectToActionResult;
            Assert.Equal("Index", action?.ActionName);
        }

        [Fact]
        public void CreateCreatesAndPersistsGroupWhenSuccesfull()
        {
            testUser.Organization = new GbOrganization();
            GroupEditViewModel groupEditViewModel = new GroupEditViewModel(_dummyContext.Test123);
            _controller.Create(groupEditViewModel, testUser);
            _groupRepository.Verify(m => m.SaveChanges(), Times.Once());
            Assert.True(testUser.Group != null);
        }

        [Fact]
        public void AddingExistingGroupThrowException()
        {
            testUser.Organization = new GbOrganization();
            _userRepository.Setup(p => p.GetBy("testUser")).Returns(testUser);
            _groupRepository.Setup(m => m.Present("Test")).Returns(true);
            GroupEditViewModel test = new GroupEditViewModel() {Name = "Test"};
            _controller.Create(test, testUser);
            Assert.True(test.Name == null);
            Assert.True(testUser.Group == null);
        }

        #endregion
        }
    }
