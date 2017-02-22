using System;
using System.Collections.Generic;

namespace GoedBezigWebApp.Models
{
    public partial class Education
    {
        public int EducationId { get; set; }
        public string School { get; set; }
        public string Specialization { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
