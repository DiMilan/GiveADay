namespace GoedBezigWebApp.Models
{
    public abstract class Organization
    {

        public int OrgId { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Btw { get; set; }
        public string Description { get; set; }

        public int? AddressId { get; set; }
        public OrganizationalAddress Address { get; set; }

    }
}
