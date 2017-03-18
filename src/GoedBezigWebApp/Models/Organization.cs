using System.Collections.Generic;

namespace GoedBezigWebApp.Models
{
    public abstract class Organization
    {

        public int OrgId { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Btw { get; set; }
        public string Description { get; set; }
        public string Domain { get; set; }

        public int? AddressId { get; set; }
        public virtual OrganizationalAddress Address { get; set; }

    }
}
