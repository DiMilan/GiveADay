using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models
{
    public class UserGroup
    {
        public int UserGroupId { get; set; }
        public User User { get; set; }
        public Group Group { get; set; }
        public bool Accepted { get; set; }

    }
}
