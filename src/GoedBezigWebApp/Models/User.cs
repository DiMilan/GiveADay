using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GoedBezigWebApp.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string FamilyName { get; set; }

    }
}
