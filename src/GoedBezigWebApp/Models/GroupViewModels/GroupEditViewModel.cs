using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models.GroupViewModels
{
    public class GroupEditViewModel
    {
        public int GroupId { get; set; }
        [Display (Name = "Groepsnaam", Prompt ="Naam van de groep")]
        [Required(ErrorMessage = "Een groep moet een naam hebben")]
        [StringLength(50,ErrorMessage = "{0} mag niet langer zijn dan 50 karakters")]
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
        [Display(Name = "Gesloten Groep?")]
        public bool ClosedGroup { get; set; }

        public GroupEditViewModel()
        {
            
        }

        public GroupEditViewModel(Group group)
        {
            GroupId = group.GroupId;
            Name = group.Name;
            Timestamp = group.Timestamp;
            ClosedGroup = group.ClosedGroup;
        }
    }
}
