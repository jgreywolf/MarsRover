using MarsRover.Framework.Interfaces;

namespace MarsRover.Framework.Commands
{
    public class NullCommand : ICommand
    {
        public Coordinates Move(Coordinates coordinates, Coordinates gridBounds)
        {
            return default;
        }

        public Direction TurnLeft()
        {
            return default;
        }

        public Direction TurnRight()
        {
            return default;
        }
    }
}
