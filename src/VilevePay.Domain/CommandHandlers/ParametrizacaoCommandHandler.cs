using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VilevePay.Domain.Commands.Parametrizacao;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;
using VilevePay.Domain.Interfaces;
using VilevePay.Domain.Responses;
using VilevePay.Infra.CrossCutting.Io.Http;

namespace VilevePay.Domain.CommandHandlers
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
        IRequestHandler<ObterSexoCommand, object>,
        IRequestHandler<ObterTipoParentescoCommand, object>,
        IRequestHandler<ObterTipoPagamentoCommand, object>,
        IRequestHandler<ObterTipoConvenioCommand, object>
    {
        private readonly IHttpAppService _httpAppService;

        public ParametrizacaoCommandHandler(
            IHttpAppService httpAppService,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _httpAppService = httpAppService;
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
                var client = _httpAppService.CreateClient("http://rest.vileve.com.br/api/");
                return await Task.FromResult(await HttpClientHelper.OnGet<IEnumerable<ParametrizacaoEstadoCivil>>(client, "v1/dados-complementares/estados-civis"));
            }
            catch (Exception)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "O sistema está momentaneamente indisponível, tente novamente mais tarde."));
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
                var client = _httpAppService.CreateClient("http://rest.vileve.com.br/api/");
                return await Task.FromResult(await HttpClientHelper.OnGet<IEnumerable<ParametrizacaoNacionalidade>>(client, "v1/dados-complementares/nacionalidades"));
            }
            catch (Exception)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "O sistema está momentaneamente indisponível, tente novamente mais tarde."));
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
                var client = _httpAppService.CreateClient("http://rest.vileve.com.br/api/");
                return await Task.FromResult(await HttpClientHelper.OnGet<IEnumerable<ParametrizacaoPerfilUsuario>>(client, "v1/dados-complementares/perfis-usuario"));
            }
            catch (Exception)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "O sistema está momentaneamente indisponível, tente novamente mais tarde."));
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
                var client = _httpAppService.CreateClient("http://rest.vileve.com.br/api/");
                return await Task.FromResult(await HttpClientHelper.OnGet<IEnumerable<ParametrizacaoTipoTelefone>>(client, "v1/dados-complementares/tipos-telefone"));
            }
            catch (Exception)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "O sistema está momentaneamente indisponível, tente novamente mais tarde."));
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
                var client = _httpAppService.CreateClient("http://rest.vileve.com.br/api/");
                return await Task.FromResult(await HttpClientHelper.OnGet<IEnumerable<ParametrizacaoTipoEmail>>(client, "v1/dados-complementares/tipos-email"));
            }
            catch (Exception)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "O sistema está momentaneamente indisponível, tente novamente mais tarde."));
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
                var client = _httpAppService.CreateClient("http://rest.vileve.com.br/api/");
                return await Task.FromResult(await HttpClientHelper.OnGet<IEnumerable<ParametrizacaoTipoEndereco>>(client, "v1/dados-complementares/tipos-endereco"));
            }
            catch (Exception)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "O sistema está momentaneamente indisponível, tente novamente mais tarde."));
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
                var client = _httpAppService.CreateClient("http://rest.vileve.com.br/api/");
                return await Task.FromResult(await HttpClientHelper.OnGet<IEnumerable<ParametrizacaoBanco>>(client, "v1/dados-complementares/bancos"));
            }
            catch (Exception)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "O sistema está momentaneamente indisponível, tente novamente mais tarde."));
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
                var client = _httpAppService.CreateClient("http://rest.vileve.com.br/api/");
                return await Task.FromResult(await HttpClientHelper.OnGet<IEnumerable<ParametrizacaoOperacaoBancaria>>(client, "v1/dados-complementares/operacoes-bancarias"));
            }
            catch (Exception)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "O sistema está momentaneamente indisponível, tente novamente mais tarde."));
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

            return await Task.FromResult(true);
        }

        public async Task<object> Handle(ObterTipoParentescoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public async Task<object> Handle(ObterTipoPagamentoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public async Task<object> Handle(ObterTipoConvenioCommand message, CancellationToken cancellationToken)
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