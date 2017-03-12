using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
