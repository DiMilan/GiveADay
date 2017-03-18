using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models
{
    public class OrganizationContact
    {
        public int ContactId { get; set; }
        public string Voornaam { get; set; }
        public string Naam { get; set; }
        public string Functie { get; set; }
        public string Email { get; set; }
        public ExternalOrganization Organization { get; set; }
        
        public OrganizationContact (string voornaam, string naam, string functie,string email, ExternalOrganization organization)
        {
            this.Voornaam = voornaam;
            this.Naam = naam;
            this.Functie = functie;
            this.Email = email;
            this.Organization = organization;
        }

        public OrganizationContact () { }
    }
}
