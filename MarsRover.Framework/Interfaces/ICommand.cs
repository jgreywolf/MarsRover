namespace MarsRover.Framework.Interfaces
{
    public interface ICommand
    {
        Direction TurnLeft();
        Direction TurnRight();
        Coordinates Move(Coordinates newCoordinates, Coordinates gridBounds);
    }
}
