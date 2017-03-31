using GoedBezigWebApp.Controllers;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Repositories;
using GoedBezigWebApp.Tests.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;
using Moq;

namespace GoedBezigWebApp.Tests.Controllers
{
    public class UserControllerTest
    {
        private readonly UserController _controller;
        private readonly Mock<IOrganizationRepository> _organizationRepository;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IGroupRepository> _groupRepository;

        private readonly Mock<UserManager<User>> _userManager;
        private readonly DummyGoedBezigDbContext _dummyContext;

        public UserControllerTest()
        {
            _dummyContext = new DummyGoedBezigDbContext();
            _userRepository = new Mock<IUserRepository>();
            _controller = new UserController(_userRepository.Object, _groupRepository.Object, null)
            {
        
            TempData = new Mock<ITempDataDictionary>().Object
            };
        }
    }
}
