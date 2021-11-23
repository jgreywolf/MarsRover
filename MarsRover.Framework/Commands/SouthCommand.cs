using MarsRover.Framework.Exceptions;
using MarsRover.Framework.Interfaces;

namespace MarsRover.Framework.Commands
{
    public class SouthCommand : ICommand
    {
        public Coordinates Move(Coordinates coordinates, Coordinates gridBounds)
        {
            var newY = coordinates.YPos - 1;
            var newCoords = new Coordinates(coordinates.XPos, newY);

            if (newY > gridBounds.YPos || newY < 0)
            {
                throw new OutOfBoundsException(newCoords);
            }

            return newCoords;
        }

        public Direction TurnLeft()
        {
            return Direction.E;
        }

        public Direction TurnRight()
        {
            return Direction.W;
        }
    }
}
