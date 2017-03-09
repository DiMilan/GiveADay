using System;
using GoedBezigWebApp.Controllers;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.GroupViewModels;
using GoedBezigWebApp.Models.Repositories;
using GoedBezigWebApp.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Xunit;

namespace GoedBezigWebApp.Tests.Controllers
{
    public class GroupControllerTest
    {
        private readonly GroupController _controller;
        private readonly Mock<IGroupRepository> _groupRepository;
        private readonly DummyGoedBezigDbContext _dummyContext;
        public GroupControllerTest()
        {
            _dummyContext = new DummyGoedBezigDbContext();
            _groupRepository = new Mock<IGroupRepository>();
            _controller = new GroupController(_groupRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
        }

        #region -- Create GET --
        [Fact]
        public void CreateMustPassNewBrewerInEditViewModel()
        {
            IActionResult action = _controller.Create();
            GroupEditViewModel groupEditViewModelEvm = (action as ViewResult)?.Model as GroupEditViewModel;
            Assert.Equal(null, groupEditViewModelEvm?.Name);
        }
        #region -- Create POST --
        [Fact]
        public void CreateRedirectsToActionIndexWhenSuccessfull()
        {
            GroupEditViewModel brewerEvm = new GroupEditViewModel(new Group()
            {
                Name = "Project123",
                ClosedGroup = true,
                Timestamp = new DateTime(2017,02,15,18,52,45)
                        
            });
            RedirectToActionResult action = _controller.Create(brewerEvm) as RedirectToActionResult;
            Assert.Equal("Index", action?.ActionName);
        }

        [Fact]
        public void CreateCreatesAndPersistsGroupWhenSuccesfull()
        {
            _groupRepository.Setup(m => m.Add(It.IsAny<Group>()));
            GroupEditViewModel brewerEvm = new GroupEditViewModel(_dummyContext.Test123);
            _controller.Create(brewerEvm);
            _groupRepository.Verify(m => m.Add(It.IsAny<Group>()), Times.Once());
            _groupRepository.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void AddingExistingGroupThrowException()
        {
            _groupRepository.Setup(m => m.Present("Test")).Returns(true);
            new GroupEditViewModel() { Name = "Test" };
        }

        #endregion

        #endregion
    }
}
