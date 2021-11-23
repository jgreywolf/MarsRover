using MarsRover.Framework.Interfaces;
using System;
using System.Linq;

namespace MarsRover.Framework.InputParsers
{
    public class RoverInstructionInputParser : IInputParser
    {
        public string[] Parse(string input)
        {
            if (input.Length <= 0)
            {
                throw new ArgumentException("Invalid Input: Rover Instructions should contain at least one letter");
            }

            return ValidateInstructions(input);
        }

        private string[] ValidateInstructions(string input)
        {
            if (input.Any(x => !Constants.VALID_INSTRUCTIONS.Contains(x.ToString())))
            {
                throw new ArgumentException("Invalid Input: Instructions should only contain following letters: 'L', 'R', 'M'");
            }

            return Enumerable.Range(0, input.Length / 1).Select(i => input.Substring(i * 1, 1)).ToArray();
        }
    }
}
