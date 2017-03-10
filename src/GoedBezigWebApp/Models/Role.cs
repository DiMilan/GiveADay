using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GoedBezigWebApp.Models
{
    public class Role: IdentityRole
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
