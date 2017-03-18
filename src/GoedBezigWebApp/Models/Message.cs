﻿using System;

namespace GoedBezigWebApp.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }
        public Activity Activity { get; set; }
    }
}
