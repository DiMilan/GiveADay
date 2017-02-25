using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models.GroupViewModels
{
    public class GroupEditViewModel
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
        public bool ClosedGroup { get; set; }

        public GroupEditViewModel()
        {
            
        }

        public GroupEditViewModel(Group group)
        {
            GroupId = group.GroupId;
            Name = group.Name;
            Timestamp = group.Timestamp;
            ClosedGroup = group.ClosedGroup;
        }
    }
}
