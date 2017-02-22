using System;
using System.Collections.Generic;

namespace GoedBezigWebApp.Models
{
    public partial class NewsSubscriber
    {
        public int SubscriberId { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
    }
}
