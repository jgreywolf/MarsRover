using MarsRover.Framework.Interfaces;
using System;
using System.Linq;

namespace MarsRover.Framework.InputParsers
{
    public class RoverStartingPositionInputParser : IInputParser
    {
        public string[] Parse(string input)
        {
            if (!input.Contains(" "))
            {
                throw new FormatException("Invalid Input: should contain 2 numbers and one letter separated by spaces");
            }

            return ValidateOrientation(
                ValidateXandYCoordinateParameters(
                    SplitInputToParameters(input)));
        }

        private string[] SplitInputToParameters(string input)
        {
            var parameters = input.Split(" ");
            if (parameters.Length != 3)
            {
                throw new ArgumentException("Invalid Input: Incorrect number of parameters; should contain 2 numbers and one letter separated by spaces");
            }

            return parameters;
        }

        private string[] ValidateXandYCoordinateParameters(string[] parameters)
        {
            if (!int.TryParse(parameters[0], out _))
            {
                throw new FormatException($"Invalid Input: First parameter should be in number format");
            }

            if (!int.TryParse(parameters[1], out _))
            {
                throw new FormatException($"Invalid Input: Second parameter should be in number format");
            }

            return parameters;
        }

        private string[] ValidateOrientation(string[] parameters)
        {
            char orientationAsChar = parameters[2][0];
            var orientation = orientationAsChar.ToString();

            if (!char.IsLetter(orientationAsChar)
                || !Constants.VALID_DIRECTIONS.Any(x => x == orientation))
            {
                throw new FormatException("Invalid Input: Third parameter should be one of the following letters: 'N, W, S, E'");
            }

            parameters[2] = orientation.ToUpper();
            return parameters;
        }
    }
}
