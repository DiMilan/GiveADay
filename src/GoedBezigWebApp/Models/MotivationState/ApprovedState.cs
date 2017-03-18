using GoedBezigWebApp.Models.Exceptions;

namespace GoedBezigWebApp.Models.MotivationState
{
    public class ApprovedState:MotivationState
    {
        public ApprovedState(Group group) : base(group)
        {
        }

        public override void SaveMotivation(string motivation)
        {
            throw new MotivationException("Een goedgekeurde motivatie kan niet meer worden aangepast");
            
        }

        public override void AddCompanyDetails(string name, string address, string email, string website)
        {
            throw new MotivationException("reeds goedgekeurde bedrijfsgegevens kunnen niet worden aangepast");
        }

        public override void AddCompanyContact(string name, string surname, string email, string title)
        {
            throw new MotivationException("reeds goedgekeurde bedrijfsgegevens kunnen niet worden aangepast");
        }

        public override void SubmitMotivation()
        {
            throw new MotivationException("Een goedgekeurde motivatie kan niet opnieuw worden ingediend");
        }

        public override bool MotivationEditable()
        {
            return false;
        }

        public override bool MotivationSubmittable()
        {
            return false;
        }
    }
}
