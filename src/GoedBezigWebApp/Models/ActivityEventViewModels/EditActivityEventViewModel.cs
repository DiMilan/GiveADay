using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models.ActivityEventViewModels
{
    public class EditActivityEventViewModel
    {
        public int? Id { get; set; }
        public ActivityType Type { get; set; }
        [Required(ErrorMessage = "You need to give this activity / event a title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "You need to supply a description")]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Date { get; set; }

        public EditActivityEventViewModel()
        {
        }

        public EditActivityEventViewModel(ActivityType type)
        {
            Type = type;
        }

        public EditActivityEventViewModel(Activity activity)
        {
            Id = activity.Id;
            Title = activity.Title;
            Description = activity.Description;
            Type = activity.GetType() == typeof(Activity) ? ActivityType.Activity : ActivityType.Event;

            if (Type != ActivityType.Event) return;

            var eventObj = activity as Event;

            if (eventObj != null) Date = eventObj.Date;
        }

        public enum ActivityType
        {
            Activity,
            Event
        }

        public bool IsNew()
        {
            return Id == null;
        }

        public string GetTypeName()
        {
            return Type == ActivityType.Activity ? "Activity" : "Event";
        }
    }


}
