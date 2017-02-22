using System;
using System.Collections.Generic;

namespace GoedBezigWebApp.Models
{
    public partial class VacancyInvitation
    {
        public int InvitationId { get; set; }
        public int? VacancyId { get; set; }
        public int? UserId { get; set; }
        public DateTime? InvitationDate { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }

        public virtual User User { get; set; }
        public virtual Vacancy Vacancy { get; set; }
    }
}
