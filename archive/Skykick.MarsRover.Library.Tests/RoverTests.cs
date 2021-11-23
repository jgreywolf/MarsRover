using FluentAssertions;
using Xunit;

namespace Skykick.MarsRover.Library.Tests
{
    public class RoverTests
    {
        [Fact]
        public void WhenInitializingNewRover_ShouldCorrectlyAssignPosition()
        {
            var x = 2;
            var y = 4;
            var orientation = "N";
            var position = new Position(x, y, orientation);

            var rover = new Rover(1, position);

            var actualX = rover.Position.XPos;
            var actualY = rover.Position.YPos;
            var actualOrientation = rover.Position.Orientation;
            actualX.Should().Be(2);
            actualY.Should().Be(4);
            actualOrientation.Should().Be("N");
        }
    }
}
