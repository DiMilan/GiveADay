using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models
{
    public class GBOrganization : Organization
    {

        public List<Group> Groups { get; set; }
        public List<User> Users { get; set; }
        public bool ClosedGroups { get; set; }

        public GBOrganization()
        {
            Groups = new List<Group>();
            Users = new List<User>();
        }

        public Group AddGroup(string groupName)
        {
            Group NewGroup = new Group(groupName, this.ClosedGroups);
            Groups.Add(NewGroup);
            return NewGroup;
        }
    }
}
