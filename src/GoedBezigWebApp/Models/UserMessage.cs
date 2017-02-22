using System;
using System.Collections.Generic;

namespace GoedBezigWebApp.Models
{
    public partial class UserMessage
    {
        public int MessageId { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public DateTime SentDate { get; set; }
        public DateTime? ReadDate { get; set; }
        public string Message { get; set; }

        public virtual User FromUser { get; set; }
        public virtual User ToUser { get; set; }
    }
}
