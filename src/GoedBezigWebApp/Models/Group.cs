using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Castle.Core.Internal;
using GoedBezigWebApp.Models.Exceptions;
using GoedBezigWebApp.Models.MotivationState;
using GoedBezigWebApp.Services;

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
        public GBOrganization GBOrganization { get; set; }
        public ExternalOrganization ExternalOrganization { get; set; }
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

        public ICollection<Activity> Activities { get; set; }
        public ICollection<ActivityTask> TaskList { get; private set; }

        public Group()
        {
            Invitations = new List<Invitation>();
            MotivationStatus = new OpenState(this);
            Activities = new List<Activity>();
            TaskList = null;//no tasklist initiated yet
        }
        public Group(string groupName, bool closedGroup) : this()
        {
            GroupName = groupName;
            ClosedGroup = closedGroup;
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
            else
            {

                throw new MotivationException("De motivatie mag niet leeg zijn");

            }
        }

        public void checkMotivationCompany()
        {
            if (CompanyName == null) throw new MotivationException("Het opgegeven berijf bevat geen naam");
            if (CompanyAddress == null) throw new MotivationException("Het opgegeven berijf bevat geen adres");
            if (CompanyEmail == null) throw new MotivationException("Het opgegeven berijf bevat geen e-mailadres");
            if (CompanyWebsite == null) throw new MotivationException("Het opgegeven berijf bevat geen naam");
        }

        public bool entitledToGiveGBLabel()
        {
            return (MotivationStatus is ApprovedState && ExternalOrganization == null);
        }

        public void AddActivity(Activity activity)
        {
            activity.Group = this;
            Activities.Add(activity);
        }

        public ICollection<Activity> GetActivities()
        {
            var activities = new List<Activity>(Activities);

            activities.RemoveAll(a => a is Event);

            return activities;
        }

        public ICollection<Event> GetEvents()
        {
            return Activities.OfType<Event>().ToList();
        }

        public void InitiateTaskList()
        {
            TaskList = new List<ActivityTask>();
        }

        public void AddTask(ActivityTask task)
        {
            if (TaskList == null)
            {
                throw new TaskListException("Nog geen draaiboek geïnitialiseerd");
            }
            if (task.Description.IsNullOrEmpty()) throw new TaskListException("de omschrijving van een taak is verplicht");
            if (task.FromDateTime != DateTime.MinValue & task.ToDateTime != DateTime.MinValue)
            {
                if (task.FromDateTime < DateTime.Now) throw new TaskListException("de begintijd moet in de toekomst liggen");
                if (task.ToDateTime < DateTime.Now) throw new TaskListException("de eindtijd moet in de toekomst liggen");
            }

            if (task.Event == null) throw new TaskListException("geen event opgegeven");
            if (task.Event.Accepted == false) throw new TaskListException("Enkel goedgekeurde evenementen komen in aanmerking");
            TaskList.Add(task);
        }
    }

}
