using System;
using System.Collections.Generic;

namespace GoedBezigWebApp.Models
{
    public partial class Organization
    {
        public Organization()
        {
        }

        public int OrgId { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Btw { get; set; }
        public string Description { get; set; }

        public int? AddressId { get; set; }
        public virtual OrganizationalAddress Address { get; set; }

    }
}
