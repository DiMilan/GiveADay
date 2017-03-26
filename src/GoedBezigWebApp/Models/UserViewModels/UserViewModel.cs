using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models.UserViewModels
{
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Username { get; set; }
        public string Organization { get; set; }
        public string Group { get; set; }
        public int NrOfInvitations{ get; set; }

        public UserViewModel(User user)
        {
            FirstName = user.FirstName;
            FamilyName = user.FamilyName;
            Username = user.UserName;
            NrOfInvitations = user.Invitations.Count;
            Organization = user.Organization!=null ? user.Organization.Name : "";
            Group = user.Group != null ? user.Group.GroupName : "";

        }

        public UserViewModel()
        {
            FirstName = "";
            FamilyName = "";
            Username = "";
            Organization = "";
            NrOfInvitations = 0;
        }
    }
}
