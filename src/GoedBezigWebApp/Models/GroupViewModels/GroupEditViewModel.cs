using System;
using System.ComponentModel.DataAnnotations;

namespace GoedBezigWebApp.Models.GroupViewModels
{
    public class GroupEditViewModel
    {
        [Display(Name = "Groepsnaam", Prompt = "Geef een naam voor de groep")]
        [Required(ErrorMessage = "Een groep moet een naam hebben")]
        [StringLength(50, ErrorMessage = "{0} mag niet langer zijn dan 50 karakters")]
        [Key]
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
        [Display(Name = "Gesloten Groep?")]
        public bool ClosedGroup { get; set; }
        [Display(Name = "Motivatie")]
        public string Motivation { get; set; }

        public bool MotivationEditable { get; set; }
        public bool MotivationSubmittable { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyContactName { get; set; }
        public string CompanyContactSurname { get; set; }
        public string CompanyContactEmail { get; set; }
        public string CompanyContactTitle { get; set; }

        public GroupEditViewModel()
        {
            MotivationEditable = true;
            MotivationSubmittable = true;
        }

        public GroupEditViewModel(Group group)
        {
            Name = group.GroupName;
            Timestamp = group.Timestamp;
            ClosedGroup = group.ClosedGroup;
            Motivation = group.Motivation;
            MotivationSubmittable = group.MotivationStatus.MotivationSubmittable();
            MotivationEditable = group.MotivationStatus.MotivationEditable();
        }
    }
}
