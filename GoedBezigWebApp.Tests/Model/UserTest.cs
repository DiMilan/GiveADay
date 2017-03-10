using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
