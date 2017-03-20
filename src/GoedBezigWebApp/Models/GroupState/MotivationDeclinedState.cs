using GoedBezigWebApp.Models.Exceptions;

namespace GoedBezigWebApp.Models.GroupState
{
    public class MotivationDeclinedState : GroupState
    {
        public MotivationDeclinedState(Group @group) : base(@group)
        {
        }
        public override void SaveMotivation(string motivation)
        {

            Group.Motivation = motivation;
            ToState(new MotivationOpenState(Group));
        }

        public override void AddCompanyDetails(string name, string address, string email, string website)
        {
            Group.CompanyName = name;
            Group.CompanyAddress = address;
            Group.CompanyEmail = email;
            Group.CompanyWebsite = website;
        }

        public override void AddCompanyContact(string name, string surname, string email, string title)
        {
            Group.CompanyContactName = name;
            Group.CompanyContactSurname = surname;
            Group.CompanyContactEmail = email;
            Group.CompanyContactTitle = title;
        }

        public override void SubmitMotivation()
        {
            throw new MotivationException("Een afgekeurde motivatie kan niet worden ingediend zonder wijzigingen");
        }
    }
}
