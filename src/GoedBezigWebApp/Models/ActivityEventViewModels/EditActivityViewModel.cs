using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models.ActivityEventViewModels
{
    public class EditActivityViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "You need to give this activity / event a title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "You need to supply a description")]
        public string Description { get; set; }

        public EditActivityViewModel()
        {
            
        }

        public EditActivityViewModel(Activity activity)
        {
            Id = activity.Id;
            Title = activity.Title;
            Description = activity.Description;
        }

        public bool IsNew()
        {
            return Id == null;
        }
    }
}
