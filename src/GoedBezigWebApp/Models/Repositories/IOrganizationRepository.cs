using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GoedBezigWebApp.Models.Repositories
{
    public interface IOrganizationRepository
    {
        Organization GetBy(int organizationId);
        IEnumerable<Organization> GetAll();
        SelectList GetAllUniqueCities();
        IEnumerable<Organization> GetAllFilteredByNameAndLocation(string searchName, string searchLocation);
    }
}