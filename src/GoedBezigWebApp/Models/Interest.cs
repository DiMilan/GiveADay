using System;
using System.Collections.Generic;

namespace GoedBezigWebApp.Models
{
    public partial class Interest
    {
        public int InterestId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string NameGrey { get; set; }
        public string NameBrown { get; set; }
    }
}
