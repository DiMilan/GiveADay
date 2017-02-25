using System.Collections;
using System.Collections.Generic;

namespace GoedBezigWebApp.Models.Repositories
{
    public interface IGroupRepository
    {
        Group GetBy(int groupId);
        IEnumerable<Group> GetAll();
        void Add(Group group);
        void Delete(Group group);
        void SaveChanges();
    }
}