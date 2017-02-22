using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GoedBezigWebApp.Models
{
    public partial class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
