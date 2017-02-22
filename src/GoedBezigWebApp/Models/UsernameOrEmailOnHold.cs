using System;
using System.Collections.Generic;

namespace GoedBezigWebApp.Models
{
    public partial class UsernameOrEmailOnHold
    {
        public long Ai { get; set; }
        public string UsernameOrEmail { get; set; }
        public DateTime Time { get; set; }
    }
}
