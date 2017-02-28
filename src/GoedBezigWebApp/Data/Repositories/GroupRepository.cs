using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoedBezigWebApp.Data.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly GoedBezigDbContext _dbContext;
        private readonly DbSet<Group> _groups;
        public GroupRepository(GoedBezigDbContext dbContext)
        {
            _dbContext = dbContext;
            _groups = _dbContext.Groups;
        }

        public Group GetBy(string groupName)
        {
            return _groups.Single((g => g.Name == groupName));
        }

        public IEnumerable<Group> GetAll()
        {
            return _groups.ToList();
        }

        public void Add(Group group)
        {
            _groups.Add(group);
        }

        public void Delete(Group group)
        {
            _groups.Remove(group);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
