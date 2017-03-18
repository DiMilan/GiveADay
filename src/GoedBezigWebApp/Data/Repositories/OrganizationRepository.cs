using System.Collections.Generic;
using System.Linq;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GoedBezigWebApp.Data.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly DbSet<Organization> _organizations;
        public OrganizationRepository(ApplicationDbContext dbContext)
        {
            _organizations = dbContext.Organizations;
        }

        public Organization GetBy(int organizationId)
        {
            return _organizations.SingleOrDefault((o => o.OrgId == organizationId));
        
        }

        public IEnumerable<Organization> GetAll()
        {
            return _organizations.Include(o => o.Address).ToList();
        }

        public IEnumerable<Organization> GetAllFilteredByNameAndLocation(string searchName, string searchLocation)
        {

            if (!string.IsNullOrEmpty(searchName) && !string.IsNullOrEmpty(searchLocation))
            {
                return _organizations.Include(o => o.Address).Where(o => o.Name.Contains(searchName) && o.Address.AddressCity.Equals(searchLocation)).ToList();
            }
            if (!string.IsNullOrEmpty(searchName))
            {
                return _organizations.Include(o => o.Address).Where(o => o.Name.Contains(searchName)).ToList();
            }
            if (!string.IsNullOrEmpty(searchLocation))
            {
                return _organizations.Include(o => o.Address).Where(o => o.Address.AddressCity.Equals(searchLocation)).ToList();
            }

            return _organizations.Include(o => o.Address).ToList();
        }

        //Refactor to use SelectList instead of SelectListItem
        public SelectList GetAllUniqueCities()
        {
            IEnumerable<string> tempList = _organizations.Include(o => o.Address).Select(o => o.Address.AddressCity).ToList().Distinct().ToList();
            return new SelectList(tempList);
        }
    }
}
