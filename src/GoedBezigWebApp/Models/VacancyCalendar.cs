using System;
using System.Collections.Generic;

namespace GoedBezigWebApp.Models
{
    public partial class VacancyCalendar
    {
        public int VacancyCalendarId { get; set; }
        public int? VacancyId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public virtual Vacancy Vacancy { get; set; }
    }
}
