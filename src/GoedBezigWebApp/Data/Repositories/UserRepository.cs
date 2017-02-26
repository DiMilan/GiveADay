using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoedBezigWebApp.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GoedBezigDbContext _dbContext;
        private readonly DbSet<User> _user;
        public UserRepository(GoedBezigDbContext dbContext)
        {
            _dbContext = dbContext;
            _user = _dbContext.Users;
        }

        public User GetBy(int userId)
        {
            return _user.SingleOrDefault((u => u.UserId == userId));
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
    }
}
