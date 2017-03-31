using System;

namespace GoedBezigWebApp.Models.Exceptions
{
    public class OrganizationException : ArgumentException
    {
        public OrganizationException()
        {
            
        }

        public OrganizationException(string message) : base(message)
        {
            
        }
    }
}
