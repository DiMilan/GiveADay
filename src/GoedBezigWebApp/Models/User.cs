using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GoedBezigWebApp.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public ICollection<Invitation> Invitations { get; set; }

        public Group Group => (from invitation in Invitations where invitation.Status == InvitationStatus.Accepted select invitation.Group).FirstOrDefault();

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
    }
}
