using System;

namespace MarsRover.Framework
{
    public class Rover
    {
        private int _id;
        private Coordinates _coordinates;
        private Direction _direction;

        public int Id => _id;
        public Coordinates Coordinates => _coordinates;
        public Direction Direction => _direction;

        public Rover(int id) : this(id, new Coordinates(), Direction.N)
        {
        }

        public Rover(int id, Coordinates coords, Direction direction)
        {
            _id = Id;
            _coordinates = coords;
            _direction = direction;
        }

        public void SetCoordinates(Coordinates newCoords)
        {
            _coordinates = newCoords;
        }

        public void SetDirection(Direction direction)
        {
            _direction = direction;
        }

        public override string ToString()
        {
            return $"{_coordinates.XPos} {_coordinates.YPos} {_direction}";
        }
    }
}
