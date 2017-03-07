using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using GoedBezigWebApp.Models.GroupViewModels;

namespace GoedBezigWebApp.Models
{
    public class Group
    {
        [Key]
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
        public bool ClosedGroup { get; set; }
        public string Motivation { get; set; }
        
        public Group()
        {
            
        }

        public Group(GroupEditViewModel groupEditViewModel)
        {
            this.Name = groupEditViewModel.Name;
            this.Timestamp = DateTime.Now;
            this.ClosedGroup = groupEditViewModel.ClosedGroup;
            this.Motivation = groupEditViewModel.Motivation;
        }
    }
}
