using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models.Exceptions;

namespace GoedBezigWebApp.Models.MotivationState
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
