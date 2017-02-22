using System;
using System.Collections.Generic;

namespace GoedBezigWebApp.Models
{
    public partial class Vacancy
    {
        public Vacancy()
        {
            UserContacts = new HashSet<UserContact>();
            VacancyCalendar = new HashSet<VacancyCalendar>();
            VacancyInvitations = new HashSet<VacancyInvitation>();
            VacancySubscriptions = new HashSet<VacancySubscription>();
        }

        public int VacancyId { get; set; }
        public int OrgId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberRequired { get; set; }
        public int? OccupancyKind { get; set; }
        public string Website { get; set; }
        public string Offer { get; set; }
        public string Logo { get; set; }
        public string Banner { get; set; }
        public string AddressCountry { get; set; }
        public string AddressCapital { get; set; }
        public string AddressCity { get; set; }
        public string AddressPostalCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public DateTime? VacancyVisibilityStartDate { get; set; }
        public DateTime? VacancyVisibilityEndDate { get; set; }
        public int? Engagement { get; set; }
        public DateTime? CreateTime { get; set; }
        public short IsDeleted { get; set; }
        public short FlexibleDate { get; set; }

        public virtual ICollection<UserContact> UserContacts { get; set; }
        public virtual ICollection<VacancyCalendar> VacancyCalendar { get; set; }
        public virtual ICollection<VacancyInvitation> VacancyInvitations { get; set; }
        public virtual ICollection<VacancySubscription> VacancySubscriptions { get; set; }
        public virtual Organization Org { get; set; }
    }
}
