using System.Collections.Generic;

namespace Skykick.MarsRover.Library
{
    public static class Constants
    {
        public static string WEST = "W";
        public static string EAST = "E";
        public static string NORTH = "N";
        public static string SOUTH = "S";
        public const string MOVE = "M";
        public const string LEFT = "L";
        public const string RIGHT = "R";

        public static IReadOnlyCollection<string> VALID_DIRECTIONS = new List<string>() { WEST, EAST, NORTH, SOUTH };
        public static IReadOnlyCollection<string> VALID_INSTRUCTIONS = new List<string>() { MOVE, LEFT, RIGHT };
    }
}
