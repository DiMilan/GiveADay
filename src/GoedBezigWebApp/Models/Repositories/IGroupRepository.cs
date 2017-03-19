using System.Collections.Generic;

namespace GoedBezigWebApp.Models.Repositories
{
    public interface IGroupRepository
    {
        Group GetBy(string groupName);
        IEnumerable<Group> GetAll();
        void Add(Group group);
        void Delete(Group group);
        void SaveChanges();
        bool Present(string groupName);
        void LoadActivities(Group group);
    }
}