using FluentAssertions;
using MarsRover.Framework.InputParsers;
using MarsRover.Framework.Interfaces;
using System;
using Xunit;

namespace MarsRover.Interface.Tests.InputParsers
{
    public class GridSizeInputParserTests
    {
        [Fact]
        public void GivenInputToCreateGrid_WhenInputHasNoSpace_ThenItShouldThrowFormatException()
        {
            IInputParser handler = new GridSizeInputParser();
            Action action = () => handler.Parse("2,3");

            action.Should().Throw<FormatException>();
        }

        [Fact]
        public void GivenInputToCreateGrid_WhenAnyNonNumberCharacterIsIncluded_ThenItShouldThrowFormatException()
        {
            IInputParser handler = new GridSizeInputParser();
            Action action = () => handler.Parse("n 1");

            action.Should().Throw<FormatException>();
        }

        [Fact]
        public void GivenInputToCreateGrid_WhenInputContainsIncorrectNumberOfValues_ThenItShouldThrowArgumentException()
        {
            IInputParser handler = new GridSizeInputParser();
            Action action = () => handler.Parse("2 3 4");

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void GivenInputToProcess_WhenInputContainsValidValues_ThenItShoudReturnCorrectStringArray()
        {
            var expected = new string[] { "2", "3" };

            IInputParser handler = new GridSizeInputParser();
            var dimensions = handler.Parse("2 3");

            dimensions.Should().BeEquivalentTo(expected);
        }
    }
}
