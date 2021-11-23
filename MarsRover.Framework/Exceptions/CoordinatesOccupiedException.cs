using System;

namespace MarsRover.Framework.Exceptions
{
    [Serializable]
    public class CoordinatesOccupiedException : Exception
    {

        public CoordinatesOccupiedException(Coordinates coords) : this($"Error: Rover blocked as Coordinates {coords.XPos} {coords.YPos} already occupied by another Rover")
        {
        }

        public CoordinatesOccupiedException(string message)
            : base(message)
        {
        }
    }
}
