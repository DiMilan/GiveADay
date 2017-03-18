using System;

namespace GoedBezigWebApp.Models.MotivationState
{
    public abstract class MotivationState
    {
        public Group Group;
        protected MotivationState(Group group)
        {
            Group = group;
        }

        public abstract void SaveMotivation(string motivation);
        public abstract void AddCompanyDetails(string name, string address, string email, string website);
        public abstract void AddCompanyContact(string name, string surname, string email, string title);
        public abstract void SubmitMotivation();
        public abstract bool MotivationEditable();
        public abstract bool MotivationSubmittable();
        protected void ToState(MotivationState motivationState)
        {
            if (motivationState == null) throw new ArgumentNullException(nameof(motivationState));
            Group.MotivationStatus = motivationState;
        }
    }
}
