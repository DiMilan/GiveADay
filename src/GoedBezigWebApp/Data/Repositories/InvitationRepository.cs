using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoedBezigWebApp.Data.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly ApplicationDbContext _context;

        public InvitationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Invitation> GetForUser(User user) => _context.Invitations.Where(i => i.User == user).Include(i => i.Group);

        public void Add(Invitation invitation)
        {
            throw new NotImplementedException();
        }

        public void Update(Invitation invitation) => _context.Invitations.Update(invitation);
        

        public void SaveChanges() => _context.SaveChanges();
        

        public Invitation GetById(string userId, string groupId) => _context.Invitations.FirstOrDefault(i => i.UserId == userId && i.GroupId == groupId);
    }
}
