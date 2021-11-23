using FluentAssertions;
using MarsRover.Framework.Exceptions;
using System.Threading.Tasks;
using System;
using Xunit;

namespace MarsRover.Framework.Tests
{
    public class PlateauTests
    {
        private Plateau _plateau = new Plateau(4, 5);
        [Fact]
        public void GivenANewPlateau_WhenSettingGridDefinition_ThenDimensionsShouldBeSetAsExpected()
        {
            _plateau.XAxis.Should().Be(4);
            _plateau.YAxis.Should().Be(5);
        }

        [Fact]
        public async Task GivenACommandToAddRover_WhenCoordinatesAreOutOfBounds_ThenOutOfBoundsExceptionShouldBeThrown()
        {
            var coords = new Coordinates(5, 3);
            Func<Task> action = async () => await _plateau.AddRoverAsync(coords);

            await action.Should().ThrowAsync<OutOfBoundsException>();
        }

        [Fact]

        public async Task GivenInputToPlaceRover_WhenCoordinatesAreOccupied_ThenCoordinatesOccupiedExceptionShouldBeThrown()
        {
            var coords = new Coordinates(0, 1);
            await _plateau.AddRoverAsync(coords);
            
            Func<Task> action = async () => await _plateau.AddRoverAsync(coords);

            await action.Should().ThrowAsync<CoordinatesOccupiedException>();
        }
    }
}
