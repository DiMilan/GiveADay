using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GoedBezigWebApp.Models
{
    public partial class User : IdentityUser
    {
        public User()
        {

        }

          
        public string FirstName { get; set; }
        public string FamilyName { get; set; }

    }
}
