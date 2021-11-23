using MarsRover.Framework.Interfaces;

namespace MarsRover.Framework.Commands
{
    public class CommandFactory
    {
        public ICommand GetCommand(Direction direction)
        {
            switch(direction)
            {
                case Direction.N:
                    return new NorthCommand();
                case Direction.S:
                    return new SouthCommand();
                case Direction.E:
                    return new EastCommand();
                case Direction.W:
                    return new WestCommand();
                default:
                    return new NullCommand();
            }
        }
    }
}
