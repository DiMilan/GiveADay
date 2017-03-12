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
        public IEnumerable<SelectListItem> GetAllUniqueCities()
        {
            //As SelectListitem provides no comparator a new could be written but splitting the filtering in two parts seems shorter. Writing this one line gives a ValueBufferShaper error which is a bug in core 1.1 and below.
            List<string> tempList = _organizations.Include(o => o.Address).Select(o => o.Address.AddressCity).ToList().Distinct().ToList();
            tempList.Insert(0, "");
            return tempList.Select(c => new SelectListItem { Text = c, Value = c }).ToList();
        }
    }
}
