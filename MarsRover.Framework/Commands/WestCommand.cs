using MarsRover.Framework.Exceptions;
using MarsRover.Framework.Interfaces;

namespace MarsRover.Framework.Commands
{
    public class WestCommand : ICommand
    {
        public Coordinates Move(Coordinates coordinates, Coordinates gridBounds)
        {
            var newX = coordinates.XPos - 1;
            var newCoords = new Coordinates(newX, coordinates.YPos);

            if (newX > gridBounds.YPos || newX < 0)
            {
                throw new OutOfBoundsException(newCoords);
            }

            return newCoords;
        }

        public Direction TurnLeft()
        {
            return Direction.S;
        }

        public Direction TurnRight()
        {
            return Direction.N;
        }
    }
}
