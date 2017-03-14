using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models.Exceptions;

namespace GoedBezigWebApp.Models.MotivationStatus
{
    public class DeclinedState:MotivationState
    {
        public DeclinedState(Group @group) : base(@group)
        {
        }
        public override void SaveMotivation(string motivation)
        {
            Group.Motivation = motivation;
            ToState(new OpenState(Group));
        }

        public override void SubmitMotivation()
        {
            throw new MotivationException("Een afgekeurde motivatie kan niet worden ingediend zonder wijzigingen");
        }

        public override bool MotivationEditable()
        {
            return true;
        }

        public override bool MotivationSubmittable()
        {
            return false;
        }
    }
}
