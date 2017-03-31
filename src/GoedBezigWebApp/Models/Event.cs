using System;

namespace GoedBezigWebApp.Models
{
    public class Event : Activity
    {
        public Event()
        {
        }

        public Event(string title, string description, DateTime date) : base(title, description)
        {
            Date = date;
        }

        public DateTime Date { get; set; }
    }
}
