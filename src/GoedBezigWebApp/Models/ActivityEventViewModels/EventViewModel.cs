using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models.ActivityEventViewModels
{
    public class EventViewModel : ActivityViewModel
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM}")]
        public DateTime Date { get; set; }

        public EventViewModel(Event @event) : base(@event)
        {
            Date = @event.Date;
        }
    }
}
