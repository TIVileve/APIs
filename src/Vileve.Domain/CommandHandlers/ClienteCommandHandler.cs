using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Vileve.Domain.Commands.Cliente;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Notifications;
using Vileve.Domain.Interfaces;
using Vileve.Domain.Responses;
using Vileve.Infra.CrossCutting.Io.Http;

namespace Vileve.Domain.CommandHandlers
{
    public class ClienteCommandHandler : CommandHandler,
        IRequestHandler<CadastrarClienteCommand, object>,
        IRequestHandler<ObterProdutoCommand, object>,
        IRequestHandler<CadastrarProdutoCommand, bool>,
        IRequestHandler<CadastrarEnderecoCommand, bool>,
        IRequestHandler<ObterDependenteCommand, object>,
        IRequestHandler<ObterDependentePorIdCommand, object>,
        IRequestHandler<CadastrarDependenteCommand, bool>,
        IRequestHandler<DeletarDependenteCommand, bool>,
        IRequestHandler<CadastrarPagamentoCommand, bool>,
        IRequestHandler<ObterCalculoMensalCommand, object>
    {
        private readonly IHttpAppService _httpAppService;
        private readonly ILogger<ClienteCommandHandler> _logger;

        public ClienteCommandHandler(
            IHttpAppService httpAppService,
            ILogger<ClienteCommandHandler> logger,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _httpAppService = httpAppService;
            _logger = logger;
        }

        public async Task<object> Handle(CadastrarClienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
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
                return await Task.FromResult(await _httpAppService.OnGet<SeguroProduto>(client, message.RequestId, "v1/proposta/seguro/produtos"));
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e, JsonSerializer.Serialize(new
                {
                    message.RequestId,
                    e.Message
                }));

                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "O sistema está momentaneamente indisponível, tente novamente mais tarde.", message));
                return await Task.FromResult(false);
            }
        }

        public Task<bool> Handle(CadastrarProdutoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(CadastrarEnderecoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public async Task<object> Handle(ObterDependenteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public async Task<object> Handle(ObterDependentePorIdCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public Task<bool> Handle(CadastrarDependenteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(DeletarDependenteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(CadastrarPagamentoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public async Task<object> Handle(ObterCalculoMensalCommand message, CancellationToken cancellationToken)
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