using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Vileve.Domain.Commands.Parametrizacao;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Notifications;
using Vileve.Domain.Interfaces;
using Vileve.Domain.Responses;
using Vileve.Infra.CrossCutting.Io.Http;

namespace Vileve.Domain.CommandHandlers
{
    public class ParametrizacaoCommandHandler : CommandHandler,
        IRequestHandler<ObterEstadoCivilCommand, object>,
        IRequestHandler<ObterNacionalidadeCommand, object>,
        IRequestHandler<ObterPerfilUsuarioCommand, object>,
        IRequestHandler<ObterTipoTelefoneCommand, object>,
        IRequestHandler<ObterTipoEmailCommand, object>,
        IRequestHandler<ObterTipoEnderecoCommand, object>,
        IRequestHandler<ObterBancoCommand, object>,
        IRequestHandler<ObterOperacaoBancariaCommand, object>,
        IRequestHandler<ObterSexoCommand, object>
    {
        private readonly ServiceManager _serviceManager;
        private readonly IHttpAppService _httpAppService;
        private readonly ILogger<ParametrizacaoCommandHandler> _logger;

        public ParametrizacaoCommandHandler(
            IOptions<ServiceManager> serviceManager,
            IHttpAppService httpAppService,
            ILogger<ParametrizacaoCommandHandler> logger,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _serviceManager = serviceManager.Value;
            _httpAppService = httpAppService;
            _logger = logger;
        }

        public async Task<object> Handle(ObterEstadoCivilCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            try
            {
                var client = _httpAppService.CreateClient(_serviceManager.UrlVileve);
                return await Task.FromResult(await _httpAppService.OnGet<IEnumerable<ParametrizacaoEstadoCivil>>(client, message.RequestId, "v1/dados-complementares/estados-civis"));
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

        public async Task<object> Handle(ObterNacionalidadeCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            try
            {
                var client = _httpAppService.CreateClient(_serviceManager.UrlVileve);
                return await Task.FromResult(await _httpAppService.OnGet<IEnumerable<ParametrizacaoNacionalidade>>(client, message.RequestId, "v1/dados-complementares/nacionalidades"));
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

        public async Task<object> Handle(ObterPerfilUsuarioCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            try
            {
                var client = _httpAppService.CreateClient(_serviceManager.UrlVileve);
                return await Task.FromResult(await _httpAppService.OnGet<IEnumerable<ParametrizacaoPerfilUsuario>>(client, message.RequestId, "v1/dados-complementares/perfis-usuario"));
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

        public async Task<object> Handle(ObterTipoTelefoneCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            try
            {
                var client = _httpAppService.CreateClient(_serviceManager.UrlVileve);
                return await Task.FromResult(await _httpAppService.OnGet<IEnumerable<ParametrizacaoTipoTelefone>>(client, message.RequestId, "v1/dados-complementares/tipos-telefone"));
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

        public async Task<object> Handle(ObterTipoEmailCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            try
            {
                var client = _httpAppService.CreateClient(_serviceManager.UrlVileve);
                return await Task.FromResult(await _httpAppService.OnGet<IEnumerable<ParametrizacaoTipoEmail>>(client, message.RequestId, "v1/dados-complementares/tipos-email"));
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

        public async Task<object> Handle(ObterTipoEnderecoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            try
            {
                var client = _httpAppService.CreateClient(_serviceManager.UrlVileve);
                return await Task.FromResult(await _httpAppService.OnGet<IEnumerable<ParametrizacaoTipoEndereco>>(client, message.RequestId, "v1/dados-complementares/tipos-endereco"));
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

        public async Task<object> Handle(ObterBancoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            try
            {
                var client = _httpAppService.CreateClient(_serviceManager.UrlVileve);
                return await Task.FromResult(await _httpAppService.OnGet<IEnumerable<ParametrizacaoBanco>>(client, message.RequestId, "v1/dados-complementares/bancos"));
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

        public async Task<object> Handle(ObterOperacaoBancariaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            try
            {
                var client = _httpAppService.CreateClient(_serviceManager.UrlVileve);
                return await Task.FromResult(await _httpAppService.OnGet<IEnumerable<ParametrizacaoOperacaoBancaria>>(client, message.RequestId, "v1/dados-complementares/operacoes-bancarias"));
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

        public async Task<object> Handle(ObterSexoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            try
            {
                var client = _httpAppService.CreateClient(_serviceManager.UrlVileve);
                return await Task.FromResult(await _httpAppService.OnGet<IEnumerable<ParametrizacaoSexo>>(client, message.RequestId, "v1/dados-complementares/sexos"));
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