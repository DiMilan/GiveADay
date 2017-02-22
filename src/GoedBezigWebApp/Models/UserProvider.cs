using System;
using System.Collections.Generic;

namespace GoedBezigWebApp.Models
{
    public partial class UserProvider
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public string Provider { get; set; }
        public string ProviderUid { get; set; }
    }
}
