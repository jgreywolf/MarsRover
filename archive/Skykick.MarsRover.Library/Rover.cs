namespace Skykick.MarsRover.Library
{
    public class Rover
    {
        private Position _position;
        public int Id { get; }
        public Position Position { get { return _position; } }
        
        public Rover(int id, Position position)
        {
            Id = id;
            _position = position;
        }

        public void Move(int newX, int newY)
        {
            _position.XPos = newX;
            _position.YPos = newY;
        }

        public void Turn(string newOrientation)
        {
            _position.Orientation = newOrientation;
        }
    }
}
