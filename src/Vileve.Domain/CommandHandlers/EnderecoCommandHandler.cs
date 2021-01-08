using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Vileve.Domain.Commands.Endereco;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Notifications;
using Vileve.Domain.Interfaces;
using Vileve.Domain.Responses;
using Vileve.Infra.CrossCutting.Io.Http;

namespace Vileve.Domain.CommandHandlers
{
    public class EnderecoCommandHandler : CommandHandler,
        IRequestHandler<ObterEnderecoCommand, object>
    {
        private readonly IHttpAppService _httpAppService;
        private readonly ILogger<EnderecoCommandHandler> _logger;

        public EnderecoCommandHandler(
            IHttpAppService httpAppService,
            ILogger<EnderecoCommandHandler> logger,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _httpAppService = httpAppService;
            _logger = logger;
        }

        public async Task<object> Handle(ObterEnderecoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            try
            {
                var client = _httpAppService.CreateClient("http://rest.vileve.com.br/api/");
                var enderecoCep = await _httpAppService.OnGet<EnderecoCep>(client, message.RequestId, $"v1/servico/buscar-endereco-cep/{message.Cep}");
                if (!enderecoCep.Resultado.Equals(0))
                    return await Task.FromResult(enderecoCep);

                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Cep não encontrado.", message));
                return await Task.FromResult(false);
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
    }
}