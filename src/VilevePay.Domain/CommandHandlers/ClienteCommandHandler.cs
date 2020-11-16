using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VilevePay.Domain.Commands.Cliente;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;
using VilevePay.Domain.Interfaces;
using VilevePay.Domain.Responses;
using VilevePay.Infra.CrossCutting.Io.Http;

namespace VilevePay.Domain.CommandHandlers
{
    public class ClienteCommandHandler : CommandHandler,
        IRequestHandler<CadastrarClienteCommand, bool>,
        IRequestHandler<ObterProdutoCommand, object>
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

        public async Task<object> Handle(ObterProdutoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            try
            {
                var client = _httpAppService.CreateClient("http://rest.vileve.com.br/api/");
                return await Task.FromResult(await HttpClientHelper.OnGet<SeguroProduto>(client, "v1/proposta/seguro/produtos"));
            }
            catch (Exception)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "O sistema está momentaneamente indisponível, tente novamente mais tarde."));
                return await Task.FromResult(false);
            }
        }
    }
}