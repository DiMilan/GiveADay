﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using GoedBezigWebApp.Models.GroupViewModels;
using GoedBezigWebApp.Models.MotivationStatus;

namespace GoedBezigWebApp.Models
{
    public class Group
    {
        [Key]
        public string GroupName { get; set; }
        public DateTime Timestamp { get; set; }
        public bool ClosedGroup { get; set; }
        public string Motivation { get; set; }
        public MotivationStatus.MotivationState MotivationStatus { get; set; }
        public ICollection<Invitation> Invitations { get; set; }
        public ICollection<User> Users
        {
            get { return Invitations.Where(i => i.Status.Equals(InvitationStatus.Accepted)).Select(i => i.User).ToList(); }
        }

        public Group()
        {
            Invitations= new List<Invitation>();
            MotivationStatus = new OpenState(this);
        }
        public Group(string groupName, bool ClosedGroup): this()
        {
            this.GroupName = groupName;
            this.ClosedGroup = ClosedGroup;
            Timestamp = DateTime.Now;
        }

        public void InviteUser(User user)
        {
            //ToDo add validations (eg if user is not yet present)
            Invitations.Add(new Invitation(user, this));
        }

        public void AddMotivation(string Motivation)
        {

            this.Motivation = Motivation;
        }
    }

}
