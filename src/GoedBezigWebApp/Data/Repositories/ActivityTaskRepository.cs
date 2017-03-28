using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Repositories;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GoedBezigWebApp.Data.Repositories
{
    public class ActivityTaskRepository:IActivityTaskRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<ActivityTask> _activityTasks;

        public ActivityTaskRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _activityTasks = dbContext.ActivityTasks;
        }
        public ActivityTask GetBy(int id)
        {
            return _activityTasks.Find(id);
        }

        public IEnumerable<ActivityTask> GetAll()
        {
            return _activityTasks.Include(a => a.ActivityTaskUsers).ThenInclude(u => u.User).ToList();
        }

        public void Add(ActivityTask activityTask)
        {
            _activityTasks.Add(activityTask);
        }

        public void Delete(ActivityTask activityTask)
        {
            _activityTasks.Remove(activityTask);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
