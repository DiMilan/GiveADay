using System.Collections.Generic;

namespace GoedBezigWebApp.Models
{
    public class GbOrganization : Organization
    {

        public List<Group> Groups { get; set; }
        public List<User> Users { get; set; }
        public bool ClosedGroups { get; set; }
        public string Domain { get; set; }

        public GbOrganization()
        {
            Groups = new List<Group>();
            Users = new List<User>();
        }

        public Group AddGroup(string groupName)
        {
            Group newGroup = new Group(groupName, ClosedGroups);
            Groups.Add(newGroup);
            return newGroup;
        }
    }
}
