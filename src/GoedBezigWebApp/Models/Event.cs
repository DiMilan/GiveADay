using System;
using System.Collections.Generic;

namespace GoedBezigWebApp.Models
{
    public class Event : Activity
    {
        public Event() : base()
        {
        }

        public Event(string title, string description, DateTime date, Group @group) : base(title, description, @group)
        {
            Date = date;
        }

        public DateTime Date { get; set; }
    }
}
