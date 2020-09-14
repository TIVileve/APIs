using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VilevePay.Domain.Commands.Cliente;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;
using VilevePay.Domain.Interfaces;

namespace VilevePay.Domain.CommandHandlers
{
    public class ClienteCommandHandler : CommandHandler,
        IRequestHandler<ValidarPessoaFisicaCommand, bool>,
        IRequestHandler<RegistrarComprovantePessoaFisicaCommand, bool>,
        IRequestHandler<ValidarPessoaJuridicaCommand, bool>,
        IRequestHandler<RegistrarComprovantePessoaJuridicaCommand, bool>,
        IRequestHandler<RegistrarEnderecoCommand, bool>,
        IRequestHandler<RegistrarComprovanteEnderecoCommand, bool>
    {
        public ClienteCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
        }

        public Task<bool> Handle(ValidarPessoaFisicaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RegistrarComprovantePessoaFisicaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(ValidarPessoaJuridicaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RegistrarComprovantePessoaJuridicaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RegistrarEnderecoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RegistrarComprovanteEnderecoCommand message, CancellationToken cancellationToken)
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