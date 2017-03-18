using System.Collections.Generic;
using System.Linq;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoedBezigWebApp.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<User> _user;
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _user = _dbContext.Users;
        }

        public User GetBy(string username)
        {
           return _user
                .Include(u => u.Organization)
                .SingleOrDefault((u => u.UserName == username));
        }

        public IEnumerable<User> GetAll()
        {
            return _user.ToList();
        }

        public void Add(User user)
        {
            _user.Add(user);
        }

        public void Delete(User user)
        {
            _user.Remove(user);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void LoadInvitations(User user)
        {
            _dbContext.Entry(user)
                .Collection(u => u.Invitations)
                .Query()
                .Include(i => i.Group)
                .Load();
        }

        public void LoadOrganization(User user)
        {
            _dbContext.Entry(user)
                .Reference(u => u.Organization)
                .Load();
        }
    }
}
