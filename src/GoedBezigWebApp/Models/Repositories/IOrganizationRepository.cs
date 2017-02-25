using System.Collections;
using System.Collections.Generic;

namespace GoedBezigWebApp.Models.Repositories
{
    public interface IOrganizationRepository
    {
        Organization GetBy(int organizationId);
        IEnumerable<Organization> GetAll();
    }
}