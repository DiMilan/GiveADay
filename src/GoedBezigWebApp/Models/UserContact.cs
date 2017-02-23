using System;
using System.Collections.Generic;

namespace GoedBezigWebApp.Models
{
    public partial class UserContact
    {
        public int UserContactsId { get; set; }
        public int? FromUserId { get; set; }
        public int? OrgContactId { get; set; }
        public int? ToUserId { get; set; }
        public int? SuggestedVacancyId { get; set; }
        public DateTime ContactDate { get; set; }
        public string Message { get; set; }

        public virtual User FromUser { get; set; }
        public virtual OrgContact OrgContact { get; set; }
        public virtual Vacancy SuggestedVacancy { get; set; }
        public virtual User ToUser { get; set; }

        UserContact()
        {
        }
    }
}
