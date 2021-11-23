using MarsRover.Framework.Interfaces;

namespace MarsRover.Framework.InputParsers
{
    public class InputParserFactory
    {
        public IInputParser GetInputParser(InputType inputType)
        {
            IInputParser parser = null;
            switch (inputType)
            {
                case InputType.PlateauSize:
                    parser = new GridSizeInputParser();
                    break;
                case InputType.RoverStart:
                    parser = new RoverStartingPositionInputParser();
                    break;
                case InputType.RoverInstructions:
                    parser = new RoverInstructionInputParser();
                    break;
            }

            return parser;
        }
    }
}
