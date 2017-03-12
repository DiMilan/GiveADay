﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models.Exceptions;

namespace GoedBezigWebApp.Models.MotivationStatus
{
    public class ApprovedState:MotivationState
    {
        public ApprovedState(Group group) : base(group)
        {
            this.Name = "Approved";
            MotivationEditable = false;
            MotivationSubmittable = false;
        }

        public override void SaveMotivation(string motivation)
        {
            throw new MotivationException("Een goedgekeurde motivatie kan niet meer worden aangepast");
            
        }

        public override void SubmitMotivation()
        {
            throw new MotivationException("Een goedgekeurde motivatie kan niet opnieuw worden ingediend");
        }
    }
}
