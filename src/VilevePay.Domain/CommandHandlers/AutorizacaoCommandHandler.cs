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
        IRequestHandler<LoginCommand, object>,
        IRequestHandler<CadastrarSenhaCommand, bool>,
        IRequestHandler<ValidarCodigoConviteCommand, bool>,
        IRequestHandler<ValidarTokenSmsCommand, bool>,
        IRequestHandler<ValidarTokenEmailCommand, bool>,
        IRequestHandler<EnviarTokenSmsCommand, bool>,
        IRequestHandler<EnviarTokenEmailCommand, bool>
    {
        public AutorizacaoCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
        }

        public async Task<object> Handle(LoginCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public Task<bool> Handle(CadastrarSenhaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
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

        public Task<bool> Handle(ValidarTokenSmsCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(ValidarTokenEmailCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(EnviarTokenSmsCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(EnviarTokenEmailCommand message, CancellationToken cancellationToken)
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