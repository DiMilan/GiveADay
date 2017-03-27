using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models.Repositories
{
    interface IActivityTaskRepository
    {
        ActivityTask GetBy(int id);
        IEnumerable<ActivityTask> GetAll();
        void Add(ActivityTask activityTask);
        void Delete(ActivityTask activityTask);
        void SaveChanges();
    }
}
