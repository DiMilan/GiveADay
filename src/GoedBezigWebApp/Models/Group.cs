using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        public ICollection<Invitation> Invitations { get; set; }
        public IEnumerable<User> Users
        {
            get { return Invitations.Where(i => i.Status.Equals(InvitationStatus.Accepted)).Select(i => i.User); }
        }

        public Group()
        {
            Invitations= new List<Invitation>();
        }
        public Group(string Name, bool ClosedGroup): this()
        {
            this.Name = Name;
            this.ClosedGroup = ClosedGroup;
            Timestamp = DateTime.Now;
        }

        public void InviteUser(User user)
        {
            //ToDo add validations (eg if user is not yet present)
            Invitations.Add(new Invitation(user, this));
        }
    }
}
