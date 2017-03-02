using System;
using System.Collections.Generic;

namespace GoedBezigWebApp.Models
{
    public partial class OrganizationalAddress
    {
        public OrganizationalAddress()
        {
            Organization = new HashSet<Organization>();
        }

        public int AddressId { get; set; }
        public string AddressCountry { get; set; }
        public string AddressCapital { get; set; }
        public string AddressCity { get; set; }
        public string AddressPostalCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public virtual ICollection<Organization> Organization { get; set; }
    }
}