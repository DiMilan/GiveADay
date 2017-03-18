using System.Collections.Generic;
using System.Linq;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Exceptions;
using Xunit;

namespace GoedBezigWebApp.Tests.Model
{
    public class InvitationTest
    {
        public User User { get; set; }
        public Group Group1 { get; set; }
        public Group Group2 { get; set; }
        public Group Group3 { get; set; }

        public Invitation Invitation1 { get; set; }
        public Invitation Invitation2 { get; set; }
        public Invitation Invitation3 { get; set; }

        public InvitationTest()
        {
            User = new User();

            Group1 = new Group("1", true);
            Group2 = new Group("2", true);
            Group3 = new Group("3", true);

            Invitation1 = new Invitation(User, Group1);
            Invitation2 = new Invitation(User, Group2);
            Invitation3 = new Invitation(User, Group3);

            User.Invitations = new List<Invitation>()
            {
                Invitation1, Invitation2, Invitation3
            };
        }

        [Fact]
        public void Accept_Invitation()
        {
            Invitation1.Accept();

            Assert.Equal(Group1, User.Group);
            Assert.Equal(Invitation1.Status, InvitationStatus.Accepted);
            Assert.Equal(Invitation2.Status, InvitationStatus.Pending);
            Assert.Equal(Invitation3.Status, InvitationStatus.Pending);
        }

        [Fact]
        public void Accept_Invitation_When_Already_In_Group()
        {
            Invitation1.Accept();

            Assert.Throws<UserAlreadyInGroupException>(() => Invitation2.Accept());
        }

        [Fact]
        public void Decline_Invitation()
        {
            Invitation1.Decline();

            Assert.Null(User.Group);
            Assert.Equal(Invitation1.Status, InvitationStatus.Declined);
            Assert.Equal(Invitation2.Status, InvitationStatus.Pending);
            Assert.Equal(Invitation3.Status, InvitationStatus.Pending);
            Assert.Equal(2, User.GetPendingInvitations().Count());
        }
    }
}
