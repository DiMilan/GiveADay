using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models.ActivityEventViewModels
{
    public class ActivityViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Accepted { get; set; }

        public ActivityViewModel(Activity activity)
        {
            Id = activity.Id;
            Title = activity.Title;
            Description = activity.Description;
            Accepted = activity.Accepted;
        }
    }
}
