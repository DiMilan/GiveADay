using GoedBezigWebApp.Models.Exceptions;

namespace GoedBezigWebApp.Models.MotivationState
{
    public class SubmittedState:MotivationState
    {
        public SubmittedState(Group group):base(group)
        {
        }

        public override void SaveMotivation(string motivation)
        {
            throw new MotivationException("Een ingediende motivatie kan niet worden aangepast");
        }

        public override void AddCompanyDetails(string name, string address, string email, string website)
        {
            throw new MotivationException("reeds ingediende bedrijfsgegevens kunnen niet worden aangepast");
        }

        public override void AddCompanyContact(string name, string surname, string email, string title)
        {
            throw new MotivationException("reeds ingediende bedrijfsgegevens kunnen niet worden aangepast");
        }

        public override void SubmitMotivation()
        {
            throw new MotivationException("Een reeds ingediende motivatie kan niet opnieuw worden ingediend");
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
