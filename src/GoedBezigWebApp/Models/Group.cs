using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Castle.Core.Internal;
using GoedBezigWebApp.Models.Exceptions;
using GoedBezigWebApp.Models.MotivationState;

namespace GoedBezigWebApp.Models
{
    public class Group
    {
        [Key]
        public string GroupName { get; set; }
        public DateTime Timestamp { get; set; }
        public bool ClosedGroup { get; set; }
        public string Motivation { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyContactName { get; set; }
        public string CompanyContactSurname { get; set; }
        public string CompanyContactEmail { get; set; }
        public string CompanyContactTitle { get; set; }
        [NotMapped]
        public MotivationState.MotivationState MotivationStatus { get; set; }
        public Organization GBOrganization { get; set; }
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

        public ICollection<Event> Events { get; set; }

        public Group()
        {
            Invitations = new List<Invitation>();
            MotivationStatus = new OpenState(this);
            Events = new List<Event>();
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

        public void AddUser(User user)
        {

            Invitations.Add(new Invitation(user, this, InvitationStatus.Accepted));
        }

        private int GetNrOfWords(string s)
        {
            return s.Split(new char[] { ' ', '.', ',', '?', '!' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public void CheckMotivation(string motivation)
        {
            if (!motivation.IsNullOrEmpty())
            {
                int nrOfWords = GetNrOfWords(motivation);
                if (nrOfWords < 100 || nrOfWords > 250)
                {
                    throw new MotivationException("De motivatie moet minstens 100 en maximum 250 woorden bevatten");
                }
            }
        }

        public void checkMotivationCompany()
        {
            if(CompanyName == null) throw new MotivationException("Het opgegeven berijf bevat geen naam");
            if (CompanyAddress == null) throw new MotivationException("Het opgegeven berijf bevat geen adres");
            if (CompanyEmail == null) throw new MotivationException("Het opgegeven berijf bevat geen e-mailadres");
            if (CompanyWebsite == null) throw new MotivationException("Het opgegeven berijf bevat geen naam");
        }

        public bool entitledToGiveGBLabel()
        {
            //NOT IMPLEMENTED YET
            return (MotivationStatus is ApprovedState && GBOrganization == null);
        }
    }

}
