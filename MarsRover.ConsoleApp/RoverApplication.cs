using MarsRover.Framework;
using MarsRover.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.ConsoleApp
{
    public enum MessageType
    {
        General,
        Command,
        Error
    }

    public class RoverApplication
    {
        private readonly IMissionController _missionController;
        private InputType _currentInputType;
        private InputType _nextInputType;
        private int _nextRoverId = 1;
        private int _currentRoverId = 0;

        public RoverApplication(IMissionController missionController)
        {
            _missionController = missionController;
        }

        public async Task Run()
        {
            _currentInputType = InputType.PlateauSize;
            _nextInputType = InputType.RoverStart;

            WriteMessageToConsole($"Hit ESC to exit at any time", MessageType.General);

            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                try
                {
                    await ProcessInput();
                }
                catch (Exception ex)
                {
                    WriteMessageToConsole(ex.Message, MessageType.Error);
                    continue;
                }
            }
        }

        private async Task ProcessInput()
        {
            var input = string.Empty;
            var response = string.Empty;

            switch (_currentInputType)
            {
                case InputType.PlateauSize:
                    WriteMessageToConsole("Enter Graph Upper Right Coordinate: (format: x y)", MessageType.Command);
                    _nextInputType = InputType.RoverStart;

                    break;
                case InputType.RoverStart:
                    WriteMessageToConsole($"Rover {_nextRoverId} Starting Position: (example: 2 4 N)", MessageType.Command);
                    _nextInputType = InputType.PlateauSize;
                    _currentRoverId = _nextRoverId;
                    _nextRoverId++;

                    break;
                case InputType.RoverInstructions:
                    WriteMessageToConsole($"Rover {_currentRoverId} Movement Plan: (ex: LMLLRRMLM)", MessageType.Command);
                    _nextInputType = InputType.RoverStart;
                    
                    break;
            }

            input = Console.ReadLine();

            response = await _missionController.ProcessInputAsync(input, _currentInputType);

            if(response.Contains("Error:"))
            {
                WriteMessageToConsole(response, MessageType.Error);
            }
            else
            {
                WriteMessageToConsole(response, MessageType.General);
                _currentInputType = _nextInputType;

            }
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
