using System;

namespace GoedBezigWebApp.Models.Exceptions
{
    public class GroupExistsException : ArgumentException
    {

        public GroupExistsException()
        {

        }
        public GroupExistsException(string message) : base(message)
        {

        }
    }

    public class MotivationException : ArgumentException
    {

        public MotivationException()
        {

        }
        public MotivationException(string message) : base(message)
        {

        }
    }
}
