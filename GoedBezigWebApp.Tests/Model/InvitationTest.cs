using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models;
using Xunit;

namespace GoedBezigWebApp.Tests.Model
{
    public class InvitationTest
    {
        [Fact]
        public void AcceptInvitation()
        {
            var user = new User();

            var group1 = new Group("1", true);
            var group2 = new Group("2", true);
            var group3 = new Group("3", true);

            var invitation1 = new Invitation(user, group1);
            var invitation2 = new Invitation(user, group2);
            var invitation3 = new Invitation(user, group3);

            user.Invitations = new List<Invitation>()
            {
                invitation1, invitation2, invitation3
            };

            invitation1.Accept();

            Assert.Equal(group1, user.Group);
            Assert.Equal(invitation1.Status, InvitationStatus.Accepted);
            Assert.Equal(invitation2.Status, InvitationStatus.Pending);
            Assert.Equal(invitation3.Status, InvitationStatus.Pending);
        }


        [Fact]
        public void DeclineInvitation()
        {
            var user = new User();

            var group1 = new Group("1", true);
            var group2 = new Group("2", true);
            var group3 = new Group("3", true);

            var invitation1 = new Invitation(user, group1);
            var invitation2 = new Invitation(user, group2);
            var invitation3 = new Invitation(user, group3);

            user.Invitations = new List<Invitation>()
            {
                invitation1, invitation2, invitation3
            };
            
            invitation1.Decline();

            Assert.Null(user.Group);
            Assert.Equal(invitation1.Status, InvitationStatus.Declined);
            Assert.Equal(invitation2.Status, InvitationStatus.Pending);
            Assert.Equal(invitation3.Status, InvitationStatus.Pending);
            Assert.Equal(2, user.GetPendingInvitations().Count());
        }
    }
}
