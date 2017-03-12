using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models.Exceptions;

namespace GoedBezigWebApp.Models.MotivationStatus
{
    public class SubmittedState:MotivationState
    {
        public SubmittedState(Group group):base(group)
        {
            this.Name = "Submitted";
            MotivationEditable = false;
            MotivationSubmittable = false;
        }

        public override void SaveMotivation(string motivation)
        {
            throw new MotivationException("Een ingediende motivatie kan niet worden aangepast");
        }

        public override void SubmitMotivation()
        {
            throw new MotivationException("Een reeds ingediende motivatie kan niet worden aangepast");
        }
    }
}
