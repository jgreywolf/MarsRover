namespace MarsRover.Framework
{
    public enum InputType
    {
        PlateauSize,
        RoverStart,
        RoverInstructions
    }

    public enum Direction
    {
        None,
        N,//North
        S,//South
        E,//East
        W//West
    }

    public enum Command
    {
        Move,
        TurnLeft,
        TurnRight
    }
}
