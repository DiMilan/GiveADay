﻿using System.Collections.Generic;

namespace GoedBezigWebApp.Models
{
    public class Activity
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Accepted { get; set; }
        public ICollection<Message> Messages { get; set; }

        public Activity()
        {
            Accepted = false;
            Messages = new List<Message>();

        }

        public Activity(string title, string description) : this()
        {
            Title = title;
            Description = description;
        }
    }
}
