using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models.OrganizationViewModels
{
    public class OrganizationViewModel
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public bool IsExternal { get; set; }
        public bool HasLabel { get; set; }
        public string Motivation { get; set; }


        public OrganizationViewModel()
        {
            
        }

        public OrganizationViewModel(Organization organization)
        {
            Name = organization.Name;
            Logo = organization.Logo;
            Description = organization.Description;
            AddressLine1 = organization.Address.AddressLine1;
            AddressLine2 = organization.Address.AddressPostalCode + " " + organization.Address.AddressCountry;
            IsExternal = organization is ExternalOrganization;
        }

        public OrganizationViewModel(ExternalOrganization organization, string motivation) : this(organization)
        {
            HasLabel = organization.HasGbLabel;
            Motivation = motivation;
        }
    }
}
