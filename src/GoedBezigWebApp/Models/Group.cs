using System;
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
            Name = groupEditViewModel.Name;
            Timestamp = DateTime.Now;
            ClosedGroup = groupEditViewModel.ClosedGroup;
            Motivation = groupEditViewModel.Motivation;
        }
    }
}
