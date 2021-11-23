using MarsRover.Framework.Exceptions;
using MarsRover.Framework.Interfaces;

namespace MarsRover.Framework.Commands
{
    public class EastCommand : ICommand
    {
        public Coordinates Move(Coordinates coordinates, Coordinates gridBounds)
        {
            var newX = coordinates.XPos + 1;
            var newCoords = new Coordinates(newX, coordinates.YPos);

            if (newX > gridBounds.XPos)
            {
                throw new OutOfBoundsException(newCoords);
            }

            return newCoords;
        }

        public Direction TurnLeft()
        {
            return Direction.N;
        }

        public Direction TurnRight()
        {
            return Direction.S;
        }
    }
}
