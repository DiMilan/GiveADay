using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models.Repositories
{
    public interface IActivityRepository
    {
        Activity GetById(int id);
        void SaveChanges();
    }
}
