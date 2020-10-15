using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VilevePay.Domain.Commands.Consultor;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;
using VilevePay.Domain.Interfaces;

namespace VilevePay.Domain.CommandHandlers
{
    public class ConsultorCommandHandler : CommandHandler,
        IRequestHandler<CadastrarDadosBancariosCommand, bool>,
        IRequestHandler<CadastrarEmailCommand, bool>,
        IRequestHandler<CadastrarEnderecoCommand, bool>,
        IRequestHandler<CadastrarPessoaJuridicaCommand, bool>,
        IRequestHandler<CadastrarTelefoneCommand, bool>,
        IRequestHandler<ObterStatusOnboardingCommand, object>
    {
        public ConsultorCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
        }

        public async Task<bool> Handle(CadastrarDadosBancariosCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> Handle(CadastrarEmailCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> Handle(CadastrarEnderecoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> Handle(CadastrarPessoaJuridicaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> Handle(CadastrarTelefoneCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public async Task<object> Handle(ObterStatusOnboardingCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }
    }
}