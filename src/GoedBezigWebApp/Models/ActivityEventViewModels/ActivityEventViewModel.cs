using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models.ActivityEventViewModels
{
    public class ActivityEventViewModel
    {
        public ICollection<Activity> Activities { get; set; }
        public ICollection<Event> Events { get; set; }

        public ActivityEventViewModel(ICollection<Activity> activities, ICollection<Event> events)
        {
            Activities = activities;
            Events = events;
        }
    }
}
