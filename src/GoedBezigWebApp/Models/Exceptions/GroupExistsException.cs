using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace GoedBezigWebApp.Models.Exceptions
{
    public class GroupExistsException : ArgumentException
    {

        public GroupExistsException() : base()
        {
            
        }
        public GroupExistsException(string message) : base(message)
        {

        }
    }
}
