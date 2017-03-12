using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Migrations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GoedBezigWebApp.Models.MotivationStatus
{
    public abstract class MotivationState
    {
        public int MotivationStatusId { get; set; }
        public string Name { get; protected set; }
        public Group Group;
        public bool MotivationEditable { get; protected set; }
        public bool MotivationSubmittable { get; protected set; }
        protected MotivationState(Group group)
        {
            Group = group;
        }

        public abstract void SaveMotivation(string motivation);
        public abstract void SubmitMotivation();
        protected void ToState(MotivationState motivationState)
        {
            if (motivationState == null) throw new ArgumentNullException(nameof(motivationState));
            Group.MotivationStatus = motivationState;
        }
    }
}
