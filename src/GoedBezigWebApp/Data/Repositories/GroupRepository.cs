﻿using System.Collections.Generic;
using System.Linq;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Exceptions;
using GoedBezigWebApp.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoedBezigWebApp.Data.Repositories
{

    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Group> _groups;
        public GroupRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _groups = _dbContext.Groups;
        }

        public Group GetBy(string groupName)
        {
            return _groups.Find(groupName);
        }

        public Group GetBy(ExternalOrganization organization)
        {
            return _groups.FirstOrDefault(g => g.ExternalOrganization == organization);
        }

        public IEnumerable<Group> GetAll()
        {
            return _groups.ToList();
        }

        public void Add(Group group)
        {
            if (Present(group.GroupName)) throw new GroupExistsException();
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

        public bool Present(string groupName)
        {
            Group g = _groups.Find(groupName);
            if (g==null)
            {
                return false;
            }
            return true;
        }

        public void LoadActivities(Group @group)
        {
            _dbContext.Entry(group)
                .Collection(g => g.Activities)
                .Load();
        }

        public void LoadOrganizations(Group group)
        {
            _dbContext.Entry(group)
                .Reference (g => g.GbOrganization)
                .Load();
            _dbContext.Entry(group)
                .Reference (g => g.ExternalOrganization)
                .Load();
        }

        public void LoadUsers(Group group)
        {
            _dbContext.Entry(group)
                .Collection(g => g.Invitations)
                .Query()
                .Include(i => i.User)
                .Load();
        }
    }
}
