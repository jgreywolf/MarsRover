using FluentAssertions;
using MarsRover.Framework.InputParsers;
using MarsRover.Framework.Interfaces;
using System;
using Xunit;

namespace MarsRover.Interface.Tests.InputParsers
{
    public class RoverInstructionInputParserTests
    {
        [Fact]
        public void GivenInputToMoveRover_WhenInputContainsInvalidCharacter_ThenItShouldThrowArgumentException()
        {
            IInputParser handler = new RoverInstructionInputParser();
            Action action = () => handler.Parse("MLLLMRK");

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void GivenInputToMoveRover_WhenInputIsValid_ThenItShouldReturnArrayWithEachLetterInItsOwnIndex()
        {
            IInputParser handler = new RoverInstructionInputParser();
            var actual = handler.Parse("MLLLMRR");

            actual.Length.Should().Be(7);
        }
    }
}
