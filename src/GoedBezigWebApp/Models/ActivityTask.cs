using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GoedBezigWebApp.Models
{
    public class ActivityTask
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<ActivityTaskUser> ActivityTaskUsers { get; set; }
        public ICollection<User> Users
        {
            get { return ActivityTaskUsers.Select(i => i.User).ToList(); }
        }
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
        public Activity Activity { get; set; }
        public TaskState CurrentState { get; set; }
        [NotMapped]
        public Group Group => Activity.Group;


        public ActivityTask(string description, ICollection<User> users, Event activityEvent, TaskState currentState) :
            this(description, users, DateTime.MaxValue, DateTime.MaxValue, activityEvent, currentState)
        {
        }

        public ActivityTask(string description, ICollection<User> users, DateTime fromDateTime, DateTime toDateTime, Event activityEvent, TaskState currentState)
        {
            Description = description;
            foreach (User user in users)
            {
                ActivityTaskUsers.Add(new ActivityTaskUser(user, this));
            }
            Activity = activityEvent;
            CurrentState = currentState;
            FromDateTime = fromDateTime;
            ToDateTime = toDateTime;
        }
    }



    public enum TaskState
    {
        ToDo = 0,
        InProgress = 1,
        Done = 2
    }
}
