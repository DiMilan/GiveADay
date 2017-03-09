using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GoedBezigWebApp.Models.GroupViewModels;

namespace GoedBezigWebApp.Models
{
    public class Group
    {
        [Key]
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
        public bool ClosedGroup { get; set; }
        public string Motivation { get; set; }
        public List<User> Users { get; set; }
        public List<UserGroup> UserGroups { get; set; }

        public Group()
        {
            Users = new List<User>();
            UserGroups= new List<UserGroup>();
        }
        public Group(string Name, bool ClosedGroup): this()
        {
            this.Name = Name;
            this.ClosedGroup = ClosedGroup;
            Timestamp = DateTime.Now;
        }

        public void AddUser(User user)
        {
            //ToDo add validations (eg if user is not yet present)
            Users.Add(user);
        }
    }
}
