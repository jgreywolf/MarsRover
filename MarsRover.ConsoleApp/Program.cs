using MarsRover.Framework;
using System;

namespace MarsRover.ConsoleApp
{
    internal class Program
    {
        static int Main(string[] args)
        {
            try
            {
                MissionControllerInterface app = new MissionControllerInterface(new MissionController());
                return 1;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return -1;
            }
        }
    }
}
