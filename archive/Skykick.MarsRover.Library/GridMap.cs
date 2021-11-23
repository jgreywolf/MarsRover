using Skykick.MarsRover.Library.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Skykick.MarsRover.Library
{
    public class GridMap
    {
        // both of these variables refer to the same data.
        // 2d array to provide quicker lookup for status of a specific coordinate
        // collection used to associate instructions to specific rover
        private Rover[,] _gridMap;
        private ICollection<Rover> _roverList;

        public GridMap(int length, int width)
        {
            _gridMap = new Rover[length, width];
            _roverList = new List<Rover>();
        }

        public void AddRover(int id, int xPos, int yPos, string orientation)
        {
            var position = new Position(xPos, yPos, orientation);
            var existingRover = _roverList.SingleOrDefault(x => x.Id == id);
            if (existingRover != null)
            {
                throw new ArgumentException($"Rover already exists with id: {id}");
            }

            existingRover = _gridMap[xPos, yPos];

            if (existingRover != null)
            {
                throw new CollisionException($"Cannot set starting coordinates of Rover {id} to {xPos}, {yPos}. Coordinates occupied by Rover {existingRover.Id}");
            }

            var newRover = new Rover(id, position);

            _gridMap[xPos, yPos] = newRover;
            _roverList.Add(newRover);
        }

        public Position ProcessRoverInstructions(int id, string instructionSet)
        {
            var rover = _roverList.SingleOrDefault(x => x.Id == id);
            if (rover == null)
            {
                throw new IndexOutOfRangeException($"No rover with the id {id} exists");
            }

            foreach (var instruction in instructionSet)
            {
                var orientation = rover.Position.Orientation;

                switch (instruction.ToString())
                {
                    case Constants.MOVE:
                        var axis = orientation == Constants.EAST || orientation == Constants.WEST ? "X" : "Y";
                        var newX = rover.Position.XPos;
                        var newY = rover.Position.YPos;

                        switch(axis)
                        {
                            case "X":
                                newX = orientation == Constants.WEST ? newX - 1 : newX + 1;
                                break;
                            case "Y":
                                newY = orientation == Constants.NORTH ? newY + 1 : newY - 1;
                                break;
                        }

                        if((newX > _gridMap.GetLength(1) || newX < 0)
                            || (newY < 0 || newY > _gridMap.GetLength(0)))
                        {
                            throw new IndexOutOfRangeException($"Rover stopped at Cannot move Rover {id} to x:{newX}, Y:{newY}. These coordinates are outside the grid bounds (X: {_gridMap.GetLength(1)}, Y: {_gridMap.GetLength(0)}");
                        }

                        var existingRover = _gridMap[newX, newY];

                        if (existingRover != null
                            && rover.Id != existingRover.Id)
                        {
                            throw new CollisionException($"Cannot move Rover {id} to {newX}, {newY}. Coordinate position occupied by Rover {existingRover.Id}");
                        }

                        _gridMap[rover.Position.XPos, rover.Position.YPos] = null;
                        rover.Move(newX, newY);
                        _gridMap[newX, newY] = rover;
                        break;
                    case Constants.LEFT:
                    case Constants.RIGHT:
                        var newOrientation = InstructionParser.GetNewOrientation(orientation, instruction.ToString());
                        rover.Turn(newOrientation);
                        break;
                }
            }

            return rover.Position;
        }
    }
}
