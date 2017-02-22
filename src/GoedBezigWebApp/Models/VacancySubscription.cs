using System;
using System.Collections.Generic;

namespace GoedBezigWebApp.Models
{
    public partial class VacancySubscription
    {
        public int SubscriptionId { get; set; }
        public int? VacancyId { get; set; }
        public int? UserId { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public int? OrgRating { get; set; }
        public string OrgRatingComment { get; set; }
        public int? VolunteerRating { get; set; }
        public string VolunteerRatingComment { get; set; }

        public virtual User User { get; set; }
        public virtual Vacancy Vacancy { get; set; }
    }
}
