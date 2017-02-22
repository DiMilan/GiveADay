using System;
using System.Collections.Generic;

namespace GoedBezigWebApp.Models
{
    public partial class OrgContact
    {
        public OrgContact()
        {
            UserContacts = new HashSet<UserContact>();
        }

        public int ContactId { get; set; }
        public int? OrgId { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Function { get; set; }

        public virtual ICollection<UserContact> UserContacts { get; set; }
        public virtual Organization Org { get; set; }
    }
}
