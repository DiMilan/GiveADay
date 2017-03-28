using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models.ActivityEventViewModels
{
    public class EditEventViewModel : EditActivityViewModel
    {
        [Required(ErrorMessage = "You need to supply a date for this event")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Date { get; set; }

        public EditEventViewModel() : base()
        {
            Date = DateTime.Today;
        }

        public EditEventViewModel(Event @event) : base(@event)
        {
            Date = @event.Date;
        }
    }
}
