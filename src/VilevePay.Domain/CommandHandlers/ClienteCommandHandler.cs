using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VilevePay.Domain.Commands.Cliente;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;
using VilevePay.Domain.Interfaces;
using VilevePay.Infra.CrossCutting.Io.Http;

namespace VilevePay.Domain.CommandHandlers
{
    public class ClienteCommandHandler : CommandHandler,
        IRequestHandler<CadastrarClienteCommand, bool>
    {
        private readonly IHttpAppService _httpAppService;

        public ClienteCommandHandler(
            IHttpAppService httpAppService,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _httpAppService = httpAppService;
        }

        public Task<bool> Handle(CadastrarClienteCommand message, CancellationToken cancellationToken)
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