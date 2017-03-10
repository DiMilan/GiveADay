﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models.Repositories
{
    public interface IInvitationRepository
    {
        IEnumerable<Invitation> GetForUser(User user);
        void Add(Invitation invitation);
        void Update(Invitation invitation);
    }
}
