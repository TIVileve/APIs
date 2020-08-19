using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VilevePay.Domain.Commands.Autorizacao;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;
using VilevePay.Domain.Interfaces;

namespace VilevePay.Domain.CommandHandlers
{
    public class AutorizacaoCommandHandler : CommandHandler,
        IRequestHandler<ValidarCodigoConviteCommand, bool>
    {
        public AutorizacaoCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
        }

        public Task<bool> Handle(ValidarCodigoConviteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}