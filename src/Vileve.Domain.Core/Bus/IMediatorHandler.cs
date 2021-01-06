using System.Threading.Tasks;
using Vileve.Domain.Core.Commands;
using Vileve.Domain.Core.Events;

namespace Vileve.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task<object> SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}