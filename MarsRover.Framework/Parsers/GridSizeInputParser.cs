using MarsRover.Framework.Interfaces;
using System;

namespace MarsRover.Framework.InputParsers
{
    public class GridSizeInputParser : IInputParser
    {
        public string[] Parse(string input)
        {
            if (!input.Contains(" "))
            {
                throw new FormatException("Invalid Input: should contain 2 numbers separated by a space");
            }

            return ValidateDimensions(ValidateInputLength(input));
        }

        private string[] ValidateInputLength(string input)
        {
            var dimensions = input.Split(" ");

            if (dimensions.Length != 2)
            {
                throw new ArgumentException("Invalid Input: incorrect number of elements");
            }

            return dimensions;
        }

        private string[] ValidateDimensions(string[] dimensions)
        {
            var validHeight = int.TryParse(dimensions[0], out int x);
            var validWidth = int.TryParse(dimensions[1], out int y);

            if (!validHeight || !validWidth)
            {
                throw new FormatException("Invalid Input: both values should be numbers");
            }

            return dimensions;
        }
    }
}
