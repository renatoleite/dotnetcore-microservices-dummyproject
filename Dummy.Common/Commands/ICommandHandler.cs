using System.Threading.Tasks;

namespace Dummy.Common.Commands
{
    /// <summary>
    /// Command handler.
    /// </summary>
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
