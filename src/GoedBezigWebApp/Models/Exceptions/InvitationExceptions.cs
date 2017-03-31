using System;

namespace GoedBezigWebApp.Models.Exceptions
{
    public class UserAlreadyInGroupException : Exception
    {
        public UserAlreadyInGroupException() : base("User is already in group")
        {
        }
    }
}
