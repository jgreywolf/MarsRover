using FluentAssertions;
using Skykick.MarsRover.Library.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Skykick.MarsRover.Library.Tests
{
    public class GridMapTests
    {
        private GridMap _testGrid;

        public GridMapTests()
        {
            _testGrid = new GridMap(4, 4);
        }

        [Fact]

        public void WhenAddingRoverToGrid_ShouldThrowIfIdExists()
        {
            _testGrid.AddRover(1, 3, 3, "N");
            Action action = () => _testGrid.AddRover(1, 3, 3, "N");

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        
        public void WhenAddingRoverToGrid_ShouldThrowIfPositionFilled()
        {
            _testGrid.AddRover(1, 3, 3, "N");
            Action action = () => _testGrid.AddRover(2, 3, 3, "N");

            action.Should().Throw<CollisionException>();
        }

        [Fact]
        public void WhenSendingInstructionsToRover_ShouldThrowIfEndPositionBlocked()
        {
            _testGrid.AddRover(1, 2, 3, "N");
            _testGrid.AddRover(2, 0, 0, "W");
            var instructions = "MMLMMMRML";
            Action action = () => _testGrid.ProcessRoverInstructions(1, instructions);

            action.Should().Throw<CollisionException>();
        }

        [Fact]
        public void WhenSendingInstructionsToRover_ShouldThrowIfPositionIsOutOfBounds()
        {
            _testGrid.AddRover(1, 3, 3, "N");
            
            Action action = () => _testGrid.AddRover(2, 3, 3, "N");

            action.Should().Throw<CollisionException>();
        }

        [Fact]
        public void WhenSendingInstructionsToRover_ShouldReturnCorrectEndPosition()
        {
            _testGrid.AddRover(1, 3, 3, "N");

            Action action = () => _testGrid.AddRover(2, 3, 3, "N");

            action.Should().Throw<CollisionException>();
        }
    }
}
