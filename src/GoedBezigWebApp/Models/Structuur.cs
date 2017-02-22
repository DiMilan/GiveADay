using System;
using System.Collections.Generic;

namespace GoedBezigWebApp.Models
{
    public partial class Structuur
    {
        public Structuur()
        {
            Organization = new HashSet<Organization>();
        }

        public int StructuurId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Organization> Organization { get; set; }
    }
}
