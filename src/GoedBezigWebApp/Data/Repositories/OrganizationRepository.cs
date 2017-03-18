using System;
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
        private readonly DbSet<GBOrganization> _gbOrganizations;
        private readonly DbSet<ExternalOrganization> _externalOrganizations;
        public OrganizationRepository(ApplicationDbContext dbContext)
        {
            _organizations = dbContext.Organizations;
            _gbOrganizations = dbContext.GbOrganizations;
            _externalOrganizations = dbContext.ExternalOrganizations;
        }

        public GBOrganization GetGbOrganizationBy(int organizationId)
        {
            return _gbOrganizations.SingleOrDefault((o => o.OrgId == organizationId));
        }

        public ExternalOrganization GetExternalOrganizationBy(int organizationId)
        {
            return _externalOrganizations.SingleOrDefault((o => o.OrgId == organizationId));
        }

        public IEnumerable<Organization> GetAllGbOrganizations()
        {
            return _gbOrganizations.Include(o => o.Address).ToList();
        }

        public IEnumerable<Organization> GetAllExternalOrganizationsWithLabel()
        {
            return _externalOrganizations
                .Include(o => o.Address)
                .Where(o => o.hasGBLabel)
                .ToList();
        }

        public IEnumerable<Organization> GetAllExternalOrganizationsWithoutLabel()
        {
            return _externalOrganizations
                .Include(o => o.Address)
                .Where(o => !o.hasGBLabel)
                .ToList();
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
        public IEnumerable<Organization> GetAllGbFilteredByNameAndLocation(string searchName, string searchLocation)
        {
            if (!string.IsNullOrEmpty(searchName) && !string.IsNullOrEmpty(searchLocation))
            {
                return _gbOrganizations.Include(o => o.Address).Where(o => o.Name.Contains(searchName) && o.Address.AddressCity.Equals(searchLocation)).ToList();
            }
            if (!string.IsNullOrEmpty(searchName))
            {
                return _gbOrganizations.Include(o => o.Address).Where(o => o.Name.Contains(searchName)).ToList();
            }
            if (!string.IsNullOrEmpty(searchLocation))
            {
                return _gbOrganizations.Include(o => o.Address).Where(o => o.Address.AddressCity.Equals(searchLocation)).ToList();
            }
            return _gbOrganizations.Include(o => o.Address).ToList();
        }

        public IEnumerable<Organization> GetAllExternalWithLabelFilteredByNameAndLocation(string searchName, string searchLocation)
        {
            if (!string.IsNullOrEmpty(searchName) && !string.IsNullOrEmpty(searchLocation))
            {
                return _externalOrganizations.Include(o => o.Address).Where(o => o.hasGBLabel && o.Name.Contains(searchName) && o.Address.AddressCity.Equals(searchLocation)).ToList();
            }
            if (!string.IsNullOrEmpty(searchName))
            {
                return _externalOrganizations.Include(o => o.Address).Where(o => o.hasGBLabel && o.Name.Contains(searchName)).ToList();
            }
            if (!string.IsNullOrEmpty(searchLocation))
            {
                return _externalOrganizations.Include(o => o.Address).Where(o => o.hasGBLabel && o.Address.AddressCity.Equals(searchLocation)).ToList();
            }
            return _externalOrganizations.Include(o => o.Address).Where(o => o.hasGBLabel).ToList();
        }

        public IEnumerable<Organization> GetAllExternalWithoutLabelFilteredByNameAndLocation(string searchName, string searchLocation)
        {
            if (!string.IsNullOrEmpty(searchName) && !string.IsNullOrEmpty(searchLocation))
            {
                return _externalOrganizations.Include(o => o.Address).Where(o => !o.hasGBLabel && o.Name.Contains(searchName) && o.Address.AddressCity.Equals(searchLocation)).ToList();
            }
            if (!string.IsNullOrEmpty(searchName))
            {
                return _externalOrganizations.Include(o => o.Address).Where(o => !o.hasGBLabel && o.Name.Contains(searchName)).ToList();
            }
            if (!string.IsNullOrEmpty(searchLocation))
            {
                return _externalOrganizations.Include(o => o.Address).Where(o => !o.hasGBLabel && o.Address.AddressCity.Equals(searchLocation)).ToList();
            }
            return _externalOrganizations.Include(o => o.Address).Where(o => !o.hasGBLabel).ToList();
        }

        public SelectList GetAllGbUniqueCities()
        {
            return new SelectList(_gbOrganizations.Include(o => o.Address).Select(o => o.Address.AddressCity).ToList().Distinct().ToList());
        }

        public SelectList GetAllExternalWithLabelUniqueCities()
        {
            return new SelectList(_externalOrganizations.Include(o => o.Address).Where(o => o.hasGBLabel).Select(o => o.Address.AddressCity).ToList().Distinct().ToList());
        }

        public SelectList GetAllExternalWithoutLabelUniqueCities()
        {
            return new SelectList(_externalOrganizations.Include(o => o.Address).Where(o => !o.hasGBLabel).Select(o => o.Address.AddressCity).ToList().Distinct().ToList());
        }
    }
}
