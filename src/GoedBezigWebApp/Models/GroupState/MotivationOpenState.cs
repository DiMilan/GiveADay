namespace GoedBezigWebApp.Models.GroupState
{
    public class MotivationOpenState : Models.GroupState.GroupState
    {

        public MotivationOpenState(Group group) : base(group)
        {
        }
        public override void SaveMotivation(string motivation)
        {
            Group.Motivation = motivation;
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
            Group.CheckMotivation();
            Group.CheckMotivationCompany();
            ToState(new MotivationSubmittedState(Group));
        }
    }
}
