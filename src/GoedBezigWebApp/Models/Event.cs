﻿using System;
using System.Collections.Generic;

namespace GoedBezigWebApp.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool Accepted { get; set; }
        public ICollection<Message> Messages { get; set; }
        public virtual Group Group { get; set; }
    }
}
