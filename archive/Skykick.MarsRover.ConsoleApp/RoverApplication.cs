using Skykick.MarsRover.Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Skykick.MarsRover.ConsoleApp
{
    public enum CommandPhase
    {
        Init,
        CreateRover,
        MoveRover
    }

    public enum MessageType
    {
        General,
        Command,
        Error
    }

    public class RoverApplication
    {
        private GridMap _grid;
        // Assumption: First call will be to initialize the "grid"
        private CommandPhase _commandPhase = CommandPhase.Init;
        // Assumption: if no rovers have been created, the first will have an id of 1
        private int _currentRoverId = 1;

        public void Run()
        {
            WriteMessageToConsole($"Hit ESC to exit", MessageType.General);

            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                try
                {
                    ProcessInput();
                }
                catch (Exception ex)
                {
                    WriteMessageToConsole(ex.Message, MessageType.Error);
                    continue;
                }
            }
        }

        public void ProcessInput()
        {
            var input = string.Empty;

            switch (_commandPhase)
            {
                case CommandPhase.Init:
                    WriteMessageToConsole("Enter Graph Upper Right Coordinate: (format: x y)", MessageType.Command);

                    input = Console.ReadLine();

                    CreateGrid(input);
                    _commandPhase = CommandPhase.CreateRover;

                    break;
                case CommandPhase.CreateRover:
                    WriteMessageToConsole($"Rover {_currentRoverId} Starting Position: (example: 2 4 N)", MessageType.Command);

                    input = Console.ReadLine();

                    AddRoverToGrid(input);
                    _commandPhase = CommandPhase.MoveRover;

                    break;
                case CommandPhase.MoveRover:
                    WriteMessageToConsole($"Rover {_currentRoverId} Movement Plan: (ex: LMLLRRMLM)", MessageType.Command);
                    input = Console.ReadLine();
                    SendInstructionsToRover(input);
                    _commandPhase = CommandPhase.CreateRover;

                    break;
            }
        }

        private void CreateGrid(string input)
        {
            if (!input.Contains(" "))
            {
                throw new FormatException("Invalid input format");
            }

            var size = input.Split(" ");
            var validHeight = int.TryParse(size[0], out int x);
            var validWidth = int.TryParse(size[1], out int y);

            if (!validHeight || !validWidth)
            {
                throw new FormatException("Coordinates must be in number format");
            }

            _grid = new GridMap(x, y);
        }

        private void AddRoverToGrid(string input)
        {
            var parameters = input.Split(" ");
            if (parameters.Length != 3)
            {
                throw new FormatException("Invalid input format");
            }

            var validXPos = int.TryParse(parameters[0], out int xPos);
            var validYPos = int.TryParse(parameters[1], out int yPos);
            if (!validXPos || !validYPos)
            {
                throw new FormatException($"Coordinate values must be in number format");
            }

            var orientation = parameters[2];
            if (!Constants.VALID_DIRECTIONS.Any(x => x == orientation))
            {
                throw new FormatException("Orientation must be one of: 'N, W, S, E'");
            }
            
            _grid.AddRover(_currentRoverId, xPos, yPos, orientation);
        }

        private void SendInstructionsToRover(string instructionSet)
        {
            if (instructionSet.Any(x => !Constants.VALID_INSTRUCTIONS.Contains(x.ToString())))
            {
                throw new FormatException("Invalid instructions detected");
            }

            // Assumption that value of _currentRoverId must be the last rover added
            // dangerous using a global variable to track most recently added rover
            var response = _grid.ProcessRoverInstructions(_currentRoverId, instructionSet);
            
            WriteMessageToConsole($"Rover {_currentRoverId} Output: {response.XPos} {response.XPos} {response.Orientation}", MessageType.General);

            // This is making the assumption that after instructions are sent we are moving to the next rover
            _currentRoverId++;
        }

        private void WriteMessageToConsole(string message, MessageType messageType)
        {
            ConsoleColor color = ConsoleColor.White;
            switch(messageType)
            {
                case MessageType.Command:
                    color = ConsoleColor.Yellow;
                    break;
                case MessageType.Error:
                    color = ConsoleColor.Red;
                    break;
            }

            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
