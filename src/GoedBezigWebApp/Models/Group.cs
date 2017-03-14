using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using GoedBezigWebApp.Models.Exceptions;
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
        [NotMapped]
        public MotivationStatus.MotivationState MotivationStatus { get; set; }
        public int StateType
        {
            get
            {
                if (MotivationStatus is OpenState)
                {
                    return 0;
                }
                else if (MotivationStatus is SubmittedState)
                {
                    return 1;
                }
                else if (MotivationStatus is DeclinedState)
                {
                    return 2;
                }
                else if (MotivationStatus is ApprovedState)
                {
                    return 3;
                }
                else
                {
                    throw new NoStateException("This group has no status");
                }
            }
            set
            {
                // create EnquireState based on value
                switch (value)
                {
                    case 0:
                        MotivationStatus = new OpenState(this);
                        break;
                    case 1:
                        MotivationStatus = new SubmittedState(this);
                        break;
                    case 2:
                        MotivationStatus = new DeclinedState(this);
                        break;
                    case 3:
                        MotivationStatus = new ApprovedState(this);
                        break;
                    default:
                        throw new NoStateException("Given value does not correspond with a state");
                }
            }
        }
        public ICollection<Invitation> Invitations { get; set; }
        public ICollection<User> Users
        {
            get { return Invitations.Where(i => i.Status.Equals(InvitationStatus.Accepted)).Select(i => i.User).ToList(); }
        }

        public Group()
        {
            Invitations = new List<Invitation>();
            MotivationStatus = new OpenState(this);
        }
        public Group(string groupName, bool ClosedGroup) : this()
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
