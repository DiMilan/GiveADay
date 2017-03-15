using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Migrations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GoedBezigWebApp.Models.MotivationState
{
    public abstract class MotivationState
    {
        public int MotivationStatusId { get; set; }
        public Group Group;
        protected MotivationState(Group group)
        {
            Group = group;
        }

        public abstract void SaveMotivation(string motivation);
        public abstract void SubmitMotivation();
        public abstract bool MotivationEditable();
        public abstract bool MotivationSubmittable();
        protected void ToState(MotivationState motivationState)
        {
            if (motivationState == null) throw new ArgumentNullException(nameof(motivationState));
            Group.MotivationStatus = motivationState;
        }
    }
}
