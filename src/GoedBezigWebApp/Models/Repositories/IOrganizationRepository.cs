using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GoedBezigWebApp.Models.Repositories
{
    public interface IOrganizationRepository
    {
        Organization GetById(int id);
        GbOrganization GetGbOrganizationBy(int organizationId);
        ExternalOrganization GetExternalOrganizationBy(int organizationId);
        IEnumerable<Organization> GetAllGbOrganizations();
        IEnumerable<Organization> GetAllExternalOrganizationsWithLabel();
        IEnumerable<Organization> GetAllExternalOrganizationsWithoutLabel();
        SelectList GetAllGbUniqueCities();
        SelectList GetAllExternalWithLabelUniqueCities();
        SelectList GetAllExternalWithoutLabelUniqueCities();
        IEnumerable<Organization> GetAllGbFilteredByNameAndLocation(string searchName, string searchLocation);
        IEnumerable<Organization> GetAllExternalWithLabelFilteredByNameAndLocation(string searchName, string searchLocation);
        IEnumerable<Organization> GetAllExternalWithoutLabelFilteredByNameAndLocation(string searchName, string searchLocation);
        void SaveChanges();
    }
}