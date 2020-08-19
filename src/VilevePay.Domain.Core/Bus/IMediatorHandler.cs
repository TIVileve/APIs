using System.Threading.Tasks;
using VilevePay.Domain.Core.Commands;
using VilevePay.Domain.Core.Events;

namespace VilevePay.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task<object> SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}