using System;
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
            Group.CheckMotivation(Group.Motivation);
            Group.checkMotivationCompany();
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
