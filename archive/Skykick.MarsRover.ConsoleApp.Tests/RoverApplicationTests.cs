using FluentAssertions;
using Skykick.MarsRover.ConsoleApp;
using System;
using System.IO;
using Xunit;

namespace Skykick.MarsRover.Interface.Tests
{
    public class RoverApplicationTests
    {
        [Fact]

        public void WhenCreatingGrid_IfEitherAxisValueNotANumber_ShouldThrowFormatException()
        {
            var roverApp = new RoverApplication();
            try
            {
                using (var sw = new StringWriter())
                {
                    using (var sr = new StringReader("n 1"))
                    {
                        Console.SetOut(sw);

                        Console.SetIn(sr);

                        Action action = () => roverApp.ProcessInput();

                        action.Should().Throw<FormatException>();
                    }
                }
            }
            catch
            { }
                
        }

        [Fact]

        public void WhenCreatingGrid_IfInputIsInInvalidFormat_ShouldThrowFormatException()
        {
            var roverApp = new RoverApplication();
            try
            {
                using (var sw = new StringWriter())
                {
                    using (var sr = new StringReader("2,3"))
                    {
                        Console.SetOut(sw);

                        Console.SetIn(sr);

                        Action action = () => roverApp.ProcessInput();

                        action.Should().Throw<FormatException>();
                    }
                }
            }
            catch
            { }

        }

        [Fact]

        public void WhenAddingRoverToGrid_IfInputIsInInvalidFormat_ShouldThrowFormatException()
        {
            var roverApp = new RoverApplication();
            try
            {
                using (var sw = new StringWriter())
                {
                    var sr = new StringReader("2 3");
                    
                    Console.SetOut(sw);

                    Console.SetIn(sr);

                    Action action = () => roverApp.ProcessInput();

                    sr = new StringReader("2,3,N");

                    action.Should().Throw<FormatException>();
                }
            }
            catch
            { }

        }

        [Fact]

        public void WhenAddingRoverToGrid_IfEitherAxisValueNotANumber_ShouldThrowFormatException()
        {
            var roverApp = new RoverApplication();
            try
            {
                using (var sw = new StringWriter())
                {
                    var sr = new StringReader("2 3");

                    Console.SetOut(sw);

                    Console.SetIn(sr);

                    Action action = () => roverApp.ProcessInput();

                    sr = new StringReader("2 n N");

                    action.Should().Throw<FormatException>();
                }
            }
            catch
            { }

        }

        [Fact]

        public void WhenAddingRoverToGrid_IfOrientationIsInvalidOption_ShouldThrowFormatException()
        {
            var roverApp = new RoverApplication();
            try
            {
                using (var sw = new StringWriter())
                {
                    var sr = new StringReader("2 3");

                    Console.SetOut(sw);
                    Console.SetIn(sr);

                    roverApp.ProcessInput();

                    sr = new StringReader("1 3 x");
                    Console.SetIn(sr);

                    Action action = () => roverApp.ProcessInput();

                    action.Should().Throw<FormatException>();
                }
            }
            catch
            { }

        }

        [Fact]
        public void WhenSendingInstructions_IfInputContainsInvalidCharacter_ShouldThrowFormatException()
        {
            var roverApp = new RoverApplication();
            try
            {
                using (var sw = new StringWriter())
                {
                    Console.SetOut(sw);

                    //Create Grid
                    var sr = new StringReader("2 3");
                    Console.SetIn(sr);
                    roverApp.ProcessInput();

                    // Add new Rover
                    sr = new StringReader("1 3 N");
                    Console.SetIn(sr);
                    roverApp.ProcessInput();

                    // Send Instructions
                    sr = new StringReader("MLLLMRK");
                    Console.SetIn(sr);

                    Action action = () => roverApp.ProcessInput();

                    action.Should().Throw<FormatException>();
                }
            }
            catch
            { }

        }
    }
}
