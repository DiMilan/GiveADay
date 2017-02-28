using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace GoedBezigWebApp.Models
{
    public class Group
    {
        [Key]
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
        public bool ClosedGroup { get; set; }
        
        public Group()
        {
            
        }
    }
}
