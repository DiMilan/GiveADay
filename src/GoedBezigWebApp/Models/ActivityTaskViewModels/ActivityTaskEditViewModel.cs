using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models.ActivityTaskViewModels
{
    public class ActivityTaskEditViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Users { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FromDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ToDate { get; set; }
        public TaskState CurrentState { get; set; }
        public string Activity { get; set; }
        public int ActivityId { get; set; }

        public ActivityTaskEditViewModel()
        {
            Id = 0;
            FromDate = DateTime.Now;//default value
            ToDate = DateTime.Now.AddDays(7);//default value
        }

        public ActivityTaskEditViewModel(ActivityTask at)
        {
            Id = at.Id;
            Description = at.Description;
            Users = at.Users.Select(u => u.UserName);
            FromDate = at.FromDateTime;
            ToDate = at.ToDateTime;
            CurrentState = at.CurrentState;
            if (at.Activity != null)
            {
                Activity = at.Activity.Title;
                ActivityId = at.Activity.Id;
            }
        }


    }
}
