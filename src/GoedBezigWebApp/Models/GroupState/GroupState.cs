using System;
using GoedBezigWebApp.Models.Exceptions;

namespace GoedBezigWebApp.Models.GroupState
{
    public abstract class GroupState
    {
        public Group Group;
        protected GroupState(Group group)
        {
            Group = group;
        }

        public virtual void SaveMotivation(string motivation)
        {
            throw new MotivationException("Een ingediende motivatie kan niet worden aangepast");
        }
        public virtual void AddCompanyDetails(string name, string address, string email, string website)
        {
            throw new MotivationException("reeds ingediende bedrijfsgegevens kunnen niet worden aangepast");
        }
        public virtual void AddCompanyContact(string name, string surname, string email, string title)
        {
            throw new MotivationException("reeds ingediende bedrijfsgegevens kunnen niet worden aangepast");
        }
        public virtual void SubmitMotivation()
        {
            throw new MotivationException("Een reeds ingediende motivatie kan niet opnieuw worden ingediend");
        }
        protected virtual void ToState(GroupState motivationState)
        {
            if (motivationState == null) throw new ArgumentNullException(nameof(motivationState));
            Group.GroupState = motivationState;
        }
    }
}
