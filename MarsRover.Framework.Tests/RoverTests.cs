using FluentAssertions;
using MarsRover.Framework.Commands;
using MarsRover.Framework.Exceptions;
using System;
using Xunit;

namespace MarsRover.Framework.Tests
{
    public class RoverTests
    {
        private readonly Coordinates _gridBounds = new Coordinates(5, 5);

        [Fact]
        public void GivenANewRover_WhenCheckingPosition_ThenDefaultValuesShouldBeReturned()
        {
            var expected = new Coordinates();

            var rover = new Rover(1);

            rover.Coordinates.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(0, 0, Command.TurnLeft, Direction.N, Direction.W)]
        [InlineData(0, 0, Command.TurnLeft, Direction.W, Direction.S)]
        [InlineData(0, 0, Command.TurnLeft, Direction.S, Direction.E)]
        [InlineData(0, 0, Command.TurnLeft, Direction.E, Direction.N)]
        [InlineData(0, 0, Command.TurnRight, Direction.N, Direction.E)]
        [InlineData(0, 0, Command.TurnRight, Direction.E, Direction.S)]
        [InlineData(0, 0, Command.TurnRight, Direction.S, Direction.W)]
        [InlineData(0, 0, Command.TurnRight, Direction.W, Direction.N)]
        public void GivenARoverFacingEachDirection_WhenExecutingATurningCommand_ThenTheRoverShouldBeFacingTheCorrectDirection(int x, int y, Command commandType, Direction startingDirection, Direction expectedDirection)
        {
            var coords = new Coordinates(x, y);
            var rover = new Rover(1, coords, startingDirection);
            var command = new CommandFactory().GetCommand(startingDirection);
            rover.SetDirection(
                commandType == Command.TurnLeft 
                ? command.TurnLeft()
                : command.TurnRight()
                );

            var actual = rover.Direction;
            actual.Should().Be(expectedDirection);
        }

        [Theory]
        [InlineData(0, 0, Direction.N, 0, 1)]
        [InlineData(0, 0, Direction.E, 1, 0)]
        [InlineData(1, 1, Direction.W, 0, 1)]
        [InlineData(1, 1, Direction.S, 1, 0)]
        public void GivenARoverFacingEachDirection_WhenExecutingAMoveCommand_ThenTheRoverCoordinatesShouldBeUpdatedCorrectly(int x, int y, Direction startingDirection, int expectedX, int expectedY)
        {
            var coords = new Coordinates(x, y);
            var rover = new Rover(1, coords, startingDirection);
            var command = new CommandFactory().GetCommand(startingDirection);
            rover.SetCoordinates(command.Move(rover.Coordinates, _gridBounds));

            var actual = rover.Coordinates;
            actual.XPos.Should().Be(expectedX);
            actual.YPos.Should().Be(expectedY);
        }

        [Theory]
        [InlineData(0, 0, Direction.W)]
        [InlineData(0, 0, Direction.S)]
        [InlineData(0, 0, Direction.N)]
        [InlineData(0, 0, Direction.E)]
        public void GivenARoverFacingEachDirection_WhenExecutingAMoveCommandThatWouldMoveOutOfBounds_ThenShouldThrowException(int x, int y, Direction startingDirection)
        {
            var coords = new Coordinates(x, y);
            var rover = new Rover(1, coords, startingDirection);
            var command = new CommandFactory().GetCommand(startingDirection);

            if(startingDirection == Direction.N
                || startingDirection == Direction.E)
            {
                rover.SetCoordinates(_gridBounds);
            }

            Action action = () => rover.SetCoordinates(command.Move(rover.Coordinates, _gridBounds));
            action.Should().Throw<OutOfBoundsException>();
        }
    }
}
