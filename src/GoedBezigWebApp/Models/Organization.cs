﻿using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Razor.Chunks;

namespace GoedBezigWebApp.Models
{
    public class Organization
    {
        public Organization()
        {
            Groups = new List<Group>();
            Users = new List<User>();
        }

        public int OrgId { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Btw { get; set; }
        public string Description { get; set; }
        public string Domain { get; set; }

        public int? AddressId { get; set; }
        public bool ClosedGroups { get; set; }
        public virtual OrganizationalAddress Address { get; set; }
        public List<Group> Groups { get; set; }
        public List<User> Users { get; set; }

        public Group AddGroup(string groupName)
        {
            Group NewGroup = new Group(groupName, this.ClosedGroups);
            Groups.Add(NewGroup);
            return NewGroup;
        }

    }
}
