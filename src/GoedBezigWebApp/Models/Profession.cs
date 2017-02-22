using System;
using System.Collections.Generic;

namespace GoedBezigWebApp.Models
{
    public partial class Profession
    {
        public int ProfessionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
