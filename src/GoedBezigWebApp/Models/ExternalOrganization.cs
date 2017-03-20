using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using GoedBezigWebApp.Models.Exceptions;
using GoedBezigWebApp.Services;

namespace GoedBezigWebApp.Models
{
    public class ExternalOrganization : Organization
    {
        public bool hasGBLabel { get; set; }
        public List<OrganizationContact> Contacts { get; set; }

        public void AssignGbLabel(Group group, List<ContactRecord> notifyContacts)
        {
            List<string> mailList = new List<string>();
            bool flagNoSelectedContacts = true;
            foreach (var contact in notifyContacts)
            {
                if (contact.Selected)
                {
                    flagNoSelectedContacts = false;
                    mailList.Add(contact.Email);
                }
            }
            if (flagNoSelectedContacts) throw new OrganizationException("Please select at least one contact!");
            hasGBLabel = true;
            group.ExternalOrganization = this;
            foreach (var user in group.Users)
            {
                mailList.Add(user.Email);                
            }
            //ADD HOOFDLECTOR?
            var mailer = new AuthMessageSender();
            var sendMail = mailer.SendEmailAsync(mailList,
                "U hebt het GoedBezigLabel gekregen.",
                String.Format(
                    "Geachte,\n\nOmdat het deugd doet om een compliment te krijgen en omdat iedereen een extra hart onder de riem best kan gebruiken, nemen wij, cursisten van {0}, deel aan het initiatief ‘Goed bezig!’.\nVia het initiatief ‘Goed bezig!’ geven we een label als erkenning aan een organisatie waarvan wij vinden dat deze goed bezig is.\nWij hebben jullie, {1}, gekozen omdat {2}. \n\nJullie zijn ‘Goed bezig!’\n\nJullie krijgen niet alleen het label, we steken ook graag de handen uit de mouwen om jullie te ondersteunen. We hebben reeds enkele ideeën rond mogelijke acties, en hadden deze graag aan jullie voorgesteld.\nHet digitale label kunnen jullie hier alvast bekijken en delen via onze facebookpagina: www.facebook.com/jebentgoedbezig\n\nMet vriendelijke groet",
                    group.GBOrganization.Name, group.ExternalOrganization.Name, group.Motivation),
                String.Format(
                    "Geachte, <br><br>Omdat het deugd doet om een compliment te krijgen en omdat iedereen een extra hart onder de riem best kan gebruiken, nemen wij, cursisten van {0}, deel aan het initiatief ‘Goed bezig!’.<br>Via het initiatief ‘Goed bezig!’ geven we een label als erkenning aan een organisatie waarvan wij vinden dat deze goed bezig is.\nWij hebben jullie, {1}, gekozen omdat {2}. <br><br>Jullie zijn ‘Goed bezig!’<br><br>Jullie krijgen niet alleen het label, we steken ook graag de handen uit de mouwen om jullie te ondersteunen. We hebben reeds enkele ideeën rond mogelijke acties, en hadden deze graag aan jullie voorgesteld.<br>Het digitale label kunnen jullie hier alvast bekijken en delen via onze facebookpagina: www.facebook.com/jebentgoedbezig<br><br>Met vriendelijke groet",
                    group.GBOrganization.Name, group.ExternalOrganization.Name, group.Motivation));

        }
    }
}
