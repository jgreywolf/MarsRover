using System.Collections.Generic;
using System.Linq;

namespace Skykick.MarsRover.Library
{
    public static class InstructionParser
    {
        private static readonly IReadOnlyCollection<(string orientation, string direction, string newOrientation)> DirectionLookup = new List<(string, string, string)>
            {
                ("N", "L", "W"),
                ("W", "L", "S"),
                ("S", "L", "E"),
                ("E", "L", "N"),
                ("N", "R", "E"),
                ("E", "R", "S"),
                ("S", "R", "W"),
                ("W", "R", "N")
            };

        public static string GetNewOrientation(string currentOrientation, string direction)
        {
            return DirectionLookup.SingleOrDefault(x => x.orientation == currentOrientation && x.direction == direction).newOrientation;
        }
    }
}
