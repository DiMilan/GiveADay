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
        public String Description { get; set; }
        public String Users { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FromDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ToDate { get; set; }
        public String CurrentState { get; set; }

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
            Users = String.Join(",", at.Users.Select(u => u.UserName));
            FromDate = at.FromDateTime;
            ToDate = at.ToDateTime;
            CurrentState = at.CurrentState.ToString();
        }


    }
}
