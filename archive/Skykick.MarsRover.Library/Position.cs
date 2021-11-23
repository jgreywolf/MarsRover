namespace Skykick.MarsRover.Library
{
    public class Position
    {
        private int _xPos;
        private int _yPos;
        private string _orientation;

        public Position(int x, int y, string orientation)
        {
            _xPos = x;
            _yPos = y;
            _orientation = orientation;
        }

        public int XPos { get => _xPos; set => _xPos = value; }
        public int YPos { get => _yPos; set => _yPos = value; }
        public string Orientation { get => _orientation; set => _orientation = value; }
    }
}
