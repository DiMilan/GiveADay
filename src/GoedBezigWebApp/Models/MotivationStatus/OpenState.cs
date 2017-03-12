using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models.MotivationStatus
{
    public class OpenState : MotivationState
    {

        public OpenState(Group group) : base(group)
        {
            this.Name = "Open";
            MotivationEditable = true;
            MotivationSubmittable = true;
        }
        public override void SaveMotivation(string motivation)
        {
            Group.Motivation = motivation;

        }

        public override void SubmitMotivation()
        {
            ToState(new SubmittedState(Group));
        }
    }
}
