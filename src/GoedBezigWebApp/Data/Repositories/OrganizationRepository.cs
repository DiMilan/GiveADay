using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoedBezigWebApp.Data.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly GoedBezigDbContext _dbContext;
        private readonly DbSet<Organization> _organizations;
        public OrganizationRepository(GoedBezigDbContext dbContext)
        {
            _dbContext = dbContext;
            _organizations = _dbContext.Organization;
        }

        public Organization GetBy(int organizationId)
        {
            return _organizations.SingleOrDefault((g => g.OrgId == organizationId));
        }

        public IEnumerable<Organization> GetAll()
        {
            return _organizations.ToList();
        }
    }
}
