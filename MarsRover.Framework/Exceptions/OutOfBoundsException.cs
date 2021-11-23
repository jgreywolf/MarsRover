using System;

namespace MarsRover.Framework.Exceptions
{
    [Serializable]
    public class OutOfBoundsException : Exception
    {

        public OutOfBoundsException(Coordinates coords) : this($"Error: Rover blocked as Coordinates {coords.XPos} {coords.YPos} are outside the bounds of the plateau grid")
        {
        }

        public OutOfBoundsException(string message)
            : base(message)
        {
        }
    }
}
