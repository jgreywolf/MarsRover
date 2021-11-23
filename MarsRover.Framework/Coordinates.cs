namespace MarsRover.Framework
{
    public class Coordinates
    {
        private int _xPos;
        private int _yPos;

        public Coordinates() : this(0, 0)
        {
        }

        public Coordinates(int x, int y)
        {
            _xPos = x;
            _yPos = y;
        }

        public int XPos => _xPos;
        public int YPos => _yPos;
    }
}
