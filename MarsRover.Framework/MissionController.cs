using MarsRover.Framework.Interfaces;
using MarsRover.Framework.InputParsers;
using System;
using System.Threading.Tasks;
using MarsRover.Framework.Commands;
using MarsRover.Framework.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover.Framework
{
    public class MissionController : IMissionController
    {
        private Plateau _plateau;
        private Rover _currentRover;

        private ICollection<Rover> RoverList;

        public MissionController()
        {
            RoverList = new List<Rover>();
        }

        public async Task<string> ProcessInputAsync(string input, InputType inputType)
        {
            var response = string.Empty;

            try
            {
                IInputParser parser = new InputParserFactory().GetInputParser(inputType);
                var parameters = parser.Parse(input);

                switch (inputType)
                {
                    case InputType.PlateauSize:
                        CreatePlateau(parameters);
                        break;
                    case InputType.RoverStart:
                        await CreateAndPlaceRoverAsync(parameters);
                        response = _currentRover.ToString();
                        break;
                    case InputType.RoverInstructions:
                        SendInstructionsToRover(parameters);
                        response = _currentRover.ToString();
                        break;
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }

            return response;
        }

        private void CreatePlateau(string[] parameters)
        {
            int.TryParse(parameters[0], out int x);
            int.TryParse(parameters[1], out int y);
            _plateau = new Plateau(x, y);

            // Reset current rover just in case a new plateau is being defined - clear out existing instancces
            _currentRover = null;
        }

        private async Task CreateAndPlaceRoverAsync(string[] parameters)
        {
            var coords = new Coordinates(int.Parse(parameters[0]), int.Parse(parameters[1]));
            Enum.TryParse(parameters[2], out Direction direction);

            await _plateau.AddRoverAsync(coords);
            var lastId = RoverList
                .Select(x=>x.Id)
                .DefaultIfEmpty()
                .Max();

            if(lastId < 1)
            {
                lastId = 2;
            }

            var newRover = new Rover(lastId, coords, direction);
            _currentRover = newRover;
        }

        private void SendInstructionsToRover(string[] instructionSet)
        {
            if (_currentRover == null)
            {
                throw new NoActiveRoverException();
            }

            foreach (var instruction in instructionSet)
            {
                var direction = _currentRover.Direction;
                var command = new CommandFactory().GetCommand(direction);

                switch (instruction)
                {
                    case "M":
                        _currentRover.SetCoordinates(command.Move(_currentRover.Coordinates, _plateau.GridBounds));
                        break;
                    case "L":
                        _currentRover.SetDirection(command.TurnLeft());
                        break;
                    case "R":
                        _currentRover.SetDirection(command.TurnRight());
                        break;
                }
            }
        }
    }
}
