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
    public class MotivationAlert : ArgumentException
    {

        public MotivationAlert()
        {

        }
        public MotivationAlert(string message) : base(message)
        {

        }
    }
    public class AddUserException : ArgumentException
    {

        public AddUserException()
        {

        }
        public AddUserException(string message) : base(message)
        {

        }
    }
    public class NoStateException : ArgumentException
    {

        public NoStateException()
        {

        }
        public NoStateException(string message) : base(message)
        {

        }
    }
    public class TaskListException : ArgumentException
    {

        public TaskListException()
        {

        }
        public TaskListException(string message) : base(message)
        {

        }
    }
}
