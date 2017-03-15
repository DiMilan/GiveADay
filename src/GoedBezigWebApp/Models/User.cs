using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using GoedBezigWebApp.Migrations;
using GoedBezigWebApp.Models.Exceptions;
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

        public void RegisterInOrganization(Organization organization)
        {
            if (!this.Email.Split('@')[1].Contains(organization.Domain)) throw new OrganizationException("Your email address has to have the extension of the organization you want to be in.");
            if (this.Organization != null) throw new OrganizationException($"You are already registered in organization {this.Organization.Name}.");
            this.Organization = organization;
        }
    }
}
