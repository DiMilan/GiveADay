﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models.MotivationState
{
    public class OpenState : MotivationState
    {

        public OpenState(Group group) : base(group)
        {
        }
        public override void SaveMotivation(string motivation)
        {
            Group.Motivation = motivation;

        }

        public override void SubmitMotivation()
        {
            Group.CheckMotivation(Group.Motivation);
            ToState(new SubmittedState(Group));
        }

        public override bool MotivationEditable()
        {
            return true;
        }

        public override bool MotivationSubmittable()
        {
            return true;
        }
    }
}
