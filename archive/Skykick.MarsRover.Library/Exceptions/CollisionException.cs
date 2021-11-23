using System;

namespace Skykick.MarsRover.Library.Exceptions
{
    public class CollisionException : Exception
    {
        public CollisionException(string message) : base(message) { }
    }
}
