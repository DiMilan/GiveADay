using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models
{
    public partial class Role: IdentityRole
    {

        public string Description { get; set; }

    }
}
