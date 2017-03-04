using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GoedBezigWebApp.Models
{
    public partial class User : IdentityUser
    {
        public User()
        {
            Educations = new HashSet<Education>();
            Professions = new HashSet<Profession>();
            UserContactsFromUser = new HashSet<UserContact>();
            UserContactsToUser = new HashSet<UserContact>();
            UserMessagesFromUser = new HashSet<UserMessage>();
            UserMessagesToUser = new HashSet<UserMessage>();
            VacancyInvitations = new HashSet<VacancyInvitation>();
            VacancySubscriptions = new HashSet<VacancySubscription>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public short? AuthLevel { get; set; }
        public string Banned { get; set; }
        public string Passwd { get; set; }
        public string PasswdRecoveryCode { get; set; }
        public DateTime? PasswdRecoveryDate { get; set; }
        public DateTime? PasswdModifiedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string OauthProvider { get; set; }
        public string OauthUid { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string Language { get; set; }
        public string AddressCountry { get; set; }
        public string AddressCapital { get; set; }
        public string AddressCity { get; set; }
        public string AddressPostalCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string VolunteerLocation { get; set; }
        public int? VolunteerLocationMaxDistance { get; set; }
        public string NationalNumber { get; set; }
        public string Phone { get; set; }
        public string BankAccount { get; set; }
        public string BicCode { get; set; }
        public int? IsAgendaPublic { get; set; }
        public int? IsProfilePublic { get; set; }
        public int? IsAccountActive { get; set; }
        public int? Engagement { get; set; }
        public string ProfilePicture { get; set; }
        public string ShortIntroduction { get; set; }
        public string JobSearchDescription { get; set; }
        public string FacebookLogin { get; set; }
        public string TwitterLogin { get; set; }
        public string GoogleLogin { get; set; }
        public string YoutubeLogin { get; set; }

        public virtual ICollection<Education> Educations { get; set; }
        public virtual ICollection<Profession> Professions { get; set; }
        public virtual ICollection<UserContact> UserContactsFromUser { get; set; }
        public virtual ICollection<UserContact> UserContactsToUser { get; set; }
        public virtual ICollection<UserMessage> UserMessagesFromUser { get; set; }
        public virtual ICollection<UserMessage> UserMessagesToUser { get; set; }
        public virtual ICollection<VacancyInvitation> VacancyInvitations { get; set; }
        public virtual ICollection<VacancySubscription> VacancySubscriptions { get; set; }
    }
}
