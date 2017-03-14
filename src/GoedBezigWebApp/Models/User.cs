using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using GoedBezigWebApp.Migrations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GoedBezigWebApp.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public ICollection<Invitation> Invitations { get; set; }

        public Group Group
        {
            get
            {
                var accepted = Invitations.FirstOrDefault(i => i.Status == InvitationStatus.Accepted);
                var group = accepted?.Group;

                return @group;
            }
        }

        public Organization Organization { get; set; }

        public User LectorUser { get; set; }

        public User()
        {
            Invitations = new List<Invitation>();
        }

        public IEnumerable<Invitation> GetPendingInvitations()
        {
            return Group != null ? new List<Invitation>() : Invitations.Where(i => i.Status == InvitationStatus.Pending);
        }

        public void AcceptInvitation(Invitation invitation)
        {
            invitation.Accept();
        }

        public void DeclineInvitation(Invitation invitation)
        {
            invitation.Decline();
        }
    }
}
