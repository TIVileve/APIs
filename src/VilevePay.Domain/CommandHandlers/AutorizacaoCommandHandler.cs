using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VilevePay.Domain.Commands.Autorizacao;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;
using VilevePay.Domain.Interfaces;
using VilevePay.Domain.Models;
using VilevePay.Domain.Responses;
using VilevePay.Infra.CrossCutting.Io.Http;

namespace VilevePay.Domain.CommandHandlers
{
    public class AutorizacaoCommandHandler : CommandHandler,
        IRequestHandler<LoginCommand, object>,
        IRequestHandler<CadastrarSenhaCommand, bool>,
        IRequestHandler<ValidarCodigoConviteCommand, bool>,
        IRequestHandler<ValidarTokenSmsCommand, bool>,
        IRequestHandler<ValidarTokenEmailCommand, bool>,
        IRequestHandler<EnviarTokenSmsCommand, bool>,
        IRequestHandler<EnviarTokenEmailCommand, bool>,
        IRequestHandler<ValidarSelfieCommand, bool>
    {
        private readonly IHttpAppService _httpAppService;
        private readonly IOnboardingRepository _onboardingRepository;

        public AutorizacaoCommandHandler(
            IHttpAppService httpAppService,
            IOnboardingRepository onboardingRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _httpAppService = httpAppService;
            _onboardingRepository = onboardingRepository;
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

            var onboarding = _onboardingRepository.Find(o => o.CodigoConvite.Equals(message.CodigoConvite) && o.NumeroCelular.Equals(message.NumeroCelular)).FirstOrDefault();
            if (onboarding == null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Código do convite ou número de celular inválidos."));
                return Task.FromResult(false);
            }

            if (!onboarding.Email.Equals(message.Email))
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "E-mail não cadastrado."));
                return Task.FromResult(false);
            }

            onboarding.Senha = message.Senha;

            _onboardingRepository.Update(onboarding);

            if (Commit())
            {
            }

            return Task.FromResult(true);
        }

        public async Task<bool> Handle(ValidarCodigoConviteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            try
            {
                var client = _httpAppService.CreateClient("http://rest.vileve.com.br/api/");
                var validarConsultor = await HttpClientHelper.OnGet<ValidarConsultor>(client, $"v1/consultor/validar/{message.CodigoConvite}");
                if (!validarConsultor.Valido.Equals(false))
                    return await Task.FromResult(true);

                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Código do convite não encontrado."));
                return await Task.FromResult(false);
            }
            catch (Exception)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "O sistema está momentaneamente indisponível, tente novamente mais tarde."));
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> Handle(ValidarTokenSmsCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            try
            {
                var client = _httpAppService.CreateClient("http://rest.vileve.com.br/api/");
                var validarToken = await HttpClientHelper.OnPost<ValidarToken, object>(client, "v1/validacao-contato/validar-token", new
                {
                    token = message.CodigoToken
                });
                if (validarToken.Valido.Equals(false))
                {
                    await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Token inválido."));
                    return await Task.FromResult(false);
                }
            }
            catch (Exception)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "O sistema está momentaneamente indisponível, tente novamente mais tarde."));
                return await Task.FromResult(false);
            }

            if (message.CodigoConvite.Equals("******"))
                return await Task.FromResult(true);

            var onboarding = _onboardingRepository.Find(o => o.CodigoConvite.Equals(message.CodigoConvite) && o.NumeroCelular.Equals(message.NumeroCelular)).FirstOrDefault();
            if (onboarding != null)
                return await Task.FromResult(true);

            onboarding = new Onboarding(Guid.NewGuid())
            {
                CodigoConvite = message.CodigoConvite,
                NumeroCelular = message.NumeroCelular
            };

            _onboardingRepository.Add(onboarding);

            if (Commit())
            {
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> Handle(ValidarTokenEmailCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            var onboarding = _onboardingRepository.Find(o => o.CodigoConvite.Equals(message.CodigoConvite)).FirstOrDefault();
            if (onboarding == null)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Código do convite não encontrado."));
                return await Task.FromResult(false);
            }

            try
            {
                var client = _httpAppService.CreateClient("http://rest.vileve.com.br/api/");
                var validarToken = await HttpClientHelper.OnPost<ValidarToken, object>(client, "v1/validacao-contato/validar-token", new
                {
                    token = message.CodigoToken
                });
                if (validarToken.Valido.Equals(false))
                {
                    await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Token inválido."));
                    return await Task.FromResult(false);
                }
            }
            catch (Exception)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "O sistema está momentaneamente indisponível, tente novamente mais tarde."));
                return await Task.FromResult(false);
            }

            onboarding.Email = message.Email;

            _onboardingRepository.Update(onboarding);

            if (Commit())
            {
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> Handle(EnviarTokenSmsCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            if (!message.CodigoConvite.Equals("******"))
            {
                var onboarding = _onboardingRepository.Find(o => o.CodigoConvite.Equals(message.CodigoConvite)).FirstOrDefault();
                if (onboarding == null)
                {
                    await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Código do convite não encontrado."));
                    return await Task.FromResult(false);
                }
            }

            try
            {
                var client = _httpAppService.CreateClient("http://rest.vileve.com.br/api/");
                var enviarTokenSms = await HttpClientHelper.OnPost<EnviarTokenSms, object>(client, "v1/validacao-contato/enviar-token-sms", new
                {
                    numero_telefone = message.NumeroCelular
                });
                if (enviarTokenSms.Sucesso.Equals(false))
                {
                    await _bus.RaiseEvent(new DomainNotification(message.MessageType, "O sistema está momentaneamente indisponível, tente novamente mais tarde."));
                    return await Task.FromResult(false);
                }
            }
            catch (Exception)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "O sistema está momentaneamente indisponível, tente novamente mais tarde."));
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> Handle(EnviarTokenEmailCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            var onboarding = _onboardingRepository.Find(o => o.CodigoConvite.Equals(message.CodigoConvite)).FirstOrDefault();
            if (onboarding == null)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Código do convite não encontrado."));
                return await Task.FromResult(false);
            }

            try
            {
                var client = _httpAppService.CreateClient("http://rest.vileve.com.br/api/");
                var enviarTokenEmail = await HttpClientHelper.OnPost<EnviarTokenEmail, object>(client, "v1/validacao-contato/enviar-token-email", new
                {
                    email = message.Email
                });
                if (enviarTokenEmail.Sucesso.Equals(false))
                {
                    await _bus.RaiseEvent(new DomainNotification(message.MessageType, "O sistema está momentaneamente indisponível, tente novamente mais tarde."));
                    return await Task.FromResult(false);
                }
            }
            catch (Exception)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "O sistema está momentaneamente indisponível, tente novamente mais tarde."));
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public Task<bool> Handle(ValidarSelfieCommand message, CancellationToken cancellationToken)
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