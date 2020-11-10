using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VilevePay.Domain.Commands.Endereco;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;
using VilevePay.Domain.Interfaces;
using VilevePay.Domain.Responses;
using VilevePay.Infra.CrossCutting.Io.Http;

namespace VilevePay.Domain.CommandHandlers
{
    public class EnderecoCommandHandler : CommandHandler,
        IRequestHandler<ObterEnderecoCommand, object>
    {
        private readonly IHttpAppService _httpAppService;

        public EnderecoCommandHandler(
            IHttpAppService httpAppService,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _httpAppService = httpAppService;
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
                var enderecoCep = await HttpClientHelper.OnGet<EnderecoCep>(client, $"v1/servico/buscar-endereco-cep/{message.Cep}");
                if (!enderecoCep.Resultado.Equals(0))
                    return await Task.FromResult(enderecoCep);

                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Cep não encontrado."));
                return await Task.FromResult(false);
            }
            catch (Exception)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "O sistema está momentaneamente indisponível, tente novamente mais tarde."));
                return await Task.FromResult(false);
            }
        }
    }
}