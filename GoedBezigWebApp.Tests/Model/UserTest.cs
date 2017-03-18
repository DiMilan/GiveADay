using GoedBezigWebApp.Models;
using Xunit;

namespace GoedBezigWebApp.Tests.Model
{
    public class UserTest
    {
        [Fact]
        public void UserGroup()
        {
            var user = new User();

            Assert.Null(user.Group);
            Assert.Empty(user.Invitations);
        }
    }

}
