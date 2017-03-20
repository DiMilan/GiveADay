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
        public bool EntitledToGiveGBLabel { get; set; }
        [Display(Name = "Bedrijfsnaam")]
        public string CompanyName { get; set; }
        [Display(Name = "Adres")]
        public string CompanyAddress { get; set; }
        [Display(Name = "Website")]
        [Url]
        public string CompanyWebsite { get; set; }
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string CompanyEmail { get; set; }
        [Display(Name = "Voornaam")]
        public string CompanyContactName { get; set; }
        [Display(Name = "Familienaam")]
        public string CompanyContactSurname { get; set; }
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string CompanyContactEmail { get; set; }
        [Display(Name = "Functie")]
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
            MotivationSubmittable = group.GroupState.MotivationSubmittable();
            MotivationEditable = group.GroupState.MotivationEditable();
            CompanyName = group.CompanyName;
            CompanyAddress = group.CompanyAddress;
            CompanyEmail = group.CompanyEmail;
            CompanyWebsite = group.CompanyWebsite;
            CompanyContactEmail = group.CompanyContactEmail;
            CompanyContactName = group.CompanyContactName;
            CompanyContactSurname = group.CompanyContactSurname;
            CompanyContactTitle = group.CompanyContactTitle;
            EntitledToGiveGBLabel = group.EntitledToGiveGbLabel();
        }
    }
}
