using System.Threading.Tasks;

namespace MarsRover.Framework.Interfaces
{
    public interface IMissionController
    {
        Task<string> ProcessInputAsync(string input, InputType inputType);
    }
}
