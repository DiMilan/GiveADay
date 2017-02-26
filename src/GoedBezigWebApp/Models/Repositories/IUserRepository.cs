using System.Collections;
using System.Collections.Generic;

namespace GoedBezigWebApp.Models.Repositories
{
    public interface IUserRepository
    {
        User GetBy(int userId);
        IEnumerable<User> GetAll();
        void Add(User user);
        void Delete(User user);
        void SaveChanges();
    }
}