using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoedBezigWebApp.Models.Exceptions
{
    public class UserAlreadyInGroupException : Exception
    {
        public UserAlreadyInGroupException() : base("User is already in group")
        {
        }
    }
}
