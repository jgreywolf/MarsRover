using MarsRover.Framework;
using Nito.AsyncEx;
using System;
using System.Threading.Tasks;

namespace MarsRover.ConsoleApp
{
    internal class Program
    {
        static async Task<int> MainAsync(string[] args)
        {
            RoverApplication app = new RoverApplication(new MissionController());
            await app.Run();
        }

        static int Main(string[] args)
        {
            try
            {
                return AsyncContext.Run(() => MainAsync(args));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return -1;
            }
        }
    }
}
