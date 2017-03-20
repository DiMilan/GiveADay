using GoedBezigWebApp.Models.Exceptions;

namespace GoedBezigWebApp.Models
{
    public class Invitation
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string GroupId { get; set; }
        public Group Group { get; set; }
        public InvitationStatus Status { get; set; }

        public Invitation()
        {
            
        }

        public Invitation(User user, Group group)
        {
            User = user;
            Group = group;
            Status = InvitationStatus.Pending;
        }

        public Invitation(User user, Group group, InvitationStatus status)
        {
            User = user;
            Group = group;
            Status = status;
        }

        public void Accept()
        {
            if (User.Group != null)
            {
                throw new UserAlreadyInGroupException();
            }
            else
            {
                Status = InvitationStatus.Accepted;
            }
        }

        public void Decline()
        {
            Status = InvitationStatus.Declined;
        }
    }

    public enum InvitationStatus
    {
        Pending = 0,
        Accepted = 1,
        Declined = 2
    }
}
