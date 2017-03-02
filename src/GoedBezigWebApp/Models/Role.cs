using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models
{
    public partial class Role: IdentityRole
    {
        public Role()
        {
        }

        public Role(string role) : base(role)
        {
        }

        public string Description { get; set; }

    }
}
