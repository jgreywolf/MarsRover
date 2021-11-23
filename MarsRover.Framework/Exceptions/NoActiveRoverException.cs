using System;

namespace MarsRover.Framework.Exceptions
{
    [Serializable]
    public class NoActiveRoverException : Exception
    {
        public NoActiveRoverException() : this("Error: There is no active rover with the specified id to send instructions to")
        {
        }

        public NoActiveRoverException(string message) : base(message)
        {
        }
    }
}