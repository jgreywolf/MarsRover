using MarsRover.Framework.Exceptions;
using System.Threading.Tasks;

namespace MarsRover.Framework
{
    public class Plateau
    {
        private int[,] _grid;

        public Plateau(int xAxis, int yAxis)
        {
            _grid = new int[xAxis, yAxis];
        }

        public int XAxis => _grid.GetLength(0);

        public int YAxis => _grid.GetLength(1);

        public Coordinates GridBounds => new Coordinates(XAxis, YAxis);

        public void CheckIfOccupied(Coordinates coords)
        {
            if(_grid[coords.XPos, coords.YPos] == 1)
            {
                throw new CoordinatesOccupiedException(coords);
            }
        }

        public async Task AddRoverAsync(Coordinates coords)
        {
            if (coords.XPos > XAxis
                || coords.YPos > YAxis)
            {
                throw new OutOfBoundsException(coords);
            }

            CheckIfOccupied(coords);
            
            _grid[coords.XPos, coords.YPos] = 1;

            await Task.CompletedTask;
        }

        public void MoveRover(Coordinates oldCoords, Coordinates newCoords)
        {
            CheckIfOccupied(newCoords);
            
            _grid[oldCoords.XPos, oldCoords.YPos] = 0;
            _grid[newCoords.XPos, newCoords.YPos] = 1;
        }
    }
}
