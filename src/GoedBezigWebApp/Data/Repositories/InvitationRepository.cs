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

        public IEnumerable<Invitation> GetForUser(User user)
        {
            return _context.Invitations.Include(i => i.Group).Where(i => i.User == user);
        }

        public void Add(Invitation invitation)
        {
            throw new NotImplementedException();
        }

        public void Update(Invitation invitation)
        {
            _context.Invitations.Update(invitation);
        }
    }
}
