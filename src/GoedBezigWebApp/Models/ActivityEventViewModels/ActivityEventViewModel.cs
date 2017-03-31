using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models.ActivityEventViewModels
{
    public class ActivityEventViewModel
    {
        public ICollection<ActivityViewModel> Activities { get; set; }
        public ICollection<EventViewModel> Events { get; set; }

        public ActivityEventViewModel(ICollection<Activity> activities, ICollection<Event> events)
        {
            var activityList = new List<ActivityViewModel>(activities.Count);
            var eventList = new List<EventViewModel>(events.Count);

            activityList.AddRange(activities.Select(activity => new ActivityViewModel(activity)));
            eventList.AddRange(events.Select(@event => new EventViewModel(@event)));
            
            Activities = activityList.OrderBy(a => a.Title).ToList();
            Events = eventList.OrderBy(e => e.Date).ThenBy(e => e.Title).ToList();
        }
    }
}
