using FluentAssertions;
using MarsRover.Framework.InputParsers;
using MarsRover.Framework.Interfaces;
using System;
using Xunit;

namespace MarsRover.Interface.Tests.InputParsers
{
    public class RoverStartingPositionInputParserTests
    {
        [Fact]
        public void GivenInputToCreateRover_WhenInputHasNoSpace_ThenItShouldThrowFormatException()
        {
            IInputParser handler = new RoverStartingPositionInputParser();
            Action action = () => handler.Parse("2,3");

            action.Should().Throw<FormatException>();
        }

        [Fact]
        public void GivenInputToCreateRover_WhenInputContainsIncorrectNumberOfValues_ThenItShouldThrowArgumentException()
        {
            IInputParser handler = new RoverStartingPositionInputParser();
            Action action = () => handler.Parse("2 N");

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void GivenInputToCreateRover_WhenAnyNonNumberCharacterIsInFirstIndex_ThenItShouldThrowFormatException()
        {
            IInputParser handler = new RoverStartingPositionInputParser();
            Action action = () => handler.Parse("n 1 N");

            action.Should().Throw<FormatException>();
        }

        [Fact]
        public void GivenInputToCreateRover_WhenAnyNonNumberCharacterIsInSecondIndex_ThenItShouldThrowFormatException()
        {
            IInputParser handler = new RoverStartingPositionInputParser();
            Action action = () => handler.Parse("2 N N");

            action.Should().Throw<FormatException>();
        }

        [Fact]
        public void GivenInputToCreateRover_WhenAnyNonAlphaCharacterIsInThirdIndex_ThenItShouldThrowFormatException()
        {
            IInputParser handler = new RoverStartingPositionInputParser();
            Action action = () => handler.Parse("2 3 2");

            action.Should().Throw<FormatException>();
        }

        [Fact]
        public void GivenInputToCreateRover_WhenInputContainsValidValues_ThenItShoudReturnCorrectStringArray()
        {
            var expected = new string[] { "2", "3", "N" };

            IInputParser handler = new RoverStartingPositionInputParser();
            var dimensions = handler.Parse("2 3 N");

            dimensions.Should().BeEquivalentTo(expected);
        }
    }
}
