﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Vileve.Domain.Commands.Autorizacao;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Notifications;
using Vileve.Domain.Enums;
using Vileve.Domain.ExtensionMethods;
using Vileve.Domain.Interfaces;
using Vileve.Domain.Models;
using Vileve.Domain.Responses;
using Vileve.Infra.CrossCutting.Io.Http;

namespace Vileve.Domain.CommandHandlers
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
        private readonly ILogger<AutorizacaoCommandHandler> _logger;

        public AutorizacaoCommandHandler(
            IHttpAppService httpAppService,
            IOnboardingRepository onboardingRepository,
            ILogger<AutorizacaoCommandHandler> logger,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _httpAppService = httpAppService;
            _onboardingRepository = onboardingRepository;
            _logger = logger;
        }

        public async Task<object> Handle(LoginCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            var onboarding = _onboardingRepository.Find(o => o.Email.Equals(message.Email) && o.Senha.Equals(message.Senha)).FirstOrDefault();
            if (onboarding == null)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "E-mail ou senha inválidos.", message));
                return await Task.FromResult(false);
            }

            return await Task.FromResult(onboarding);
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
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Código do convite ou número de celular inválidos.", message));
                return Task.FromResult(false);
            }

            if (!onboarding.Email.Equals(message.Email))
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "E-mail não cadastrado.", message));
                return Task.FromResult(false);
            }

            onboarding.Senha = message.Senha;
            onboarding.StatusOnboarding = StatusOnboarding.ValidacaoSenha;

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
                var validarConsultor = await _httpAppService.OnGet<ValidarConsultor>(client, message.RequestId, $"v1/consultor/validar/{message.CodigoConvite}");
                if (!validarConsultor.Valido.Equals(false))
                    return await Task.FromResult(true);

                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Código do convite não encontrado.", message));
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
                var validarToken = await _httpAppService.OnPost<ValidarToken, object>(client, message.RequestId, "v1/validacao-contato/validar-token", new
                {
                    token = message.CodigoToken
                });
                if (validarToken.Valido.Equals(false))
                {
                    await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Token inválido.", message));
                    return await Task.FromResult(false);
                }
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

            if (message.CodigoConvite.Equals("******"))
                return await Task.FromResult(true);

            var onboarding = _onboardingRepository.Find(o => o.CodigoConvite.Equals(message.CodigoConvite) && o.NumeroCelular.Equals(message.NumeroCelular)).FirstOrDefault();
            if (onboarding != null)
                return await Task.FromResult(true);

            onboarding = new Onboarding(Guid.NewGuid())
            {
                CodigoConvite = message.CodigoConvite,
                NumeroCelular = message.NumeroCelular,
                StatusOnboarding = StatusOnboarding.ValidacaoToken
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

            var onboarding = _onboardingRepository.Find(o => o.CodigoConvite.Equals(message.CodigoConvite) && o.NumeroCelular.Equals(message.NumeroCelular)).FirstOrDefault();
            if (onboarding == null)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Código do convite ou número de celular inválidos.", message));
                return await Task.FromResult(false);
            }

            try
            {
                var client = _httpAppService.CreateClient("http://rest.vileve.com.br/api/");
                var validarToken = await _httpAppService.OnPost<ValidarToken, object>(client, message.RequestId, "v1/validacao-contato/validar-token", new
                {
                    token = message.CodigoToken
                });
                if (validarToken.Valido.Equals(false))
                {
                    await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Token inválido.", message));
                    return await Task.FromResult(false);
                }
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

            onboarding.Email = message.Email;
            onboarding.StatusOnboarding = StatusOnboarding.ValidacaoEmail;

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

            try
            {
                var client = _httpAppService.CreateClient("http://rest.vileve.com.br/api/");
                var enviarTokenSms = await _httpAppService.OnPost<EnviarTokenSms, object>(client, message.RequestId, "v1/validacao-contato/enviar-token-sms", new
                {
                    numero_telefone = message.NumeroCelular
                });
                if (enviarTokenSms.Sucesso.Equals(false))
                {
                    await _bus.RaiseEvent(new DomainNotification(message.MessageType, "O sistema está momentaneamente indisponível, tente novamente mais tarde.", message));
                    return await Task.FromResult(false);
                }
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

            return await Task.FromResult(true);
        }

        public async Task<bool> Handle(EnviarTokenEmailCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            try
            {
                var client = _httpAppService.CreateClient("http://rest.vileve.com.br/api/");
                var enviarTokenEmail = await _httpAppService.OnPost<EnviarTokenEmail, object>(client, message.RequestId, "v1/validacao-contato/enviar-token-email", new
                {
                    email = message.Email
                });
                if (enviarTokenEmail.Sucesso.Equals(false))
                {
                    await _bus.RaiseEvent(new DomainNotification(message.MessageType, "O sistema está momentaneamente indisponível, tente novamente mais tarde.", message));
                    return await Task.FromResult(false);
                }
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

            return await Task.FromResult(true);
        }

        public async Task<bool> Handle(ValidarSelfieCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            var onboarding = _onboardingRepository.Find(o => o.CodigoConvite.Equals(message.CodigoConvite) && o.NumeroCelular.Equals(message.NumeroCelular)).FirstOrDefault();
            if (onboarding == null)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Código do convite ou número de celular inválidos.", message));
                return await Task.FromResult(false);
            }

            try
            {
                var client = _httpAppService.CreateClient("http://rest.vileve.com.br/api/");

                var token = await _httpAppService.OnPost<Token, object>(client, message.RequestId, "v1/auth/login", new
                {
                    usuario = "sistemaconsulta.api",
                    senha = "123456"
                });
                if (token == null || string.IsNullOrEmpty(token.AccessToken))
                {
                    await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Usuário de integração não encontrado.", message));
                    return await Task.FromResult(false);
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

                await _httpAppService.OnPost<object, object>(client, message.RequestId, $"v1/consultor/cadastrar/pessoajuridica/{message.CodigoConvite}", new
                {
                    razao_social = onboarding.Consultor.RazaoSocial,
                    nome_fantasia = onboarding.Consultor.NomeFantasia,
                    data_fundacao = "2015-01-18",
                    codigo_nacionalidade = "1",
                    cnpj = onboarding.Consultor.Cnpj,
                    codigo_ramo_atividade = 1,
                    inscricao_municipal = onboarding.Consultor.InscricaoMunicipal,
                    inscricao_estadual = onboarding.Consultor.InscricaoEstadual,
                    pessoa = new
                    {
                        nome_completo = onboarding.Consultor.Representante.NomeCompleto,
                        apelido = onboarding.Consultor.Representante.NomeCompleto.Substring(0, onboarding.Consultor.Representante.NomeCompleto.IndexOf(" ", StringComparison.Ordinal)),
                        nome_social = onboarding.Consultor.Representante.NomeCompleto.Substring(0, onboarding.Consultor.Representante.NomeCompleto.IndexOf(" ", StringComparison.Ordinal)),
                        codigo_sexo = onboarding.Consultor.Representante.Sexo,
                        codigo_estado_civil = onboarding.Consultor.Representante.EstadoCivil,
                        data_nascimento = "1962-01-13",
                        codigo_nacionalidade = onboarding.Consultor.Representante.Nacionalidade,
                        cpf = onboarding.Consultor.Representante.Cpf,
                        autenticacao = new
                        {
                            usuario = onboarding.Email,
                            senha = onboarding.Senha.CreateMd5(),
                            codigo_perfil = 17
                        },
                        emails = onboarding.Consultor.Representante.Emails.Select(item => new
                        {
                            tipo_email = item.TipoEmail,
                            email = item.Email,
                            principal = 1
                        }),
                        telefones = onboarding.Consultor.Representante.Telefones.Select(item => new
                        {
                            tipo_telefone = item.TipoTelefone,
                            ddd = item.Numero.Substring(0, 2),
                            telefone = item.Numero.Substring(2),
                            principal = 1
                        }),
                        enderecos = onboarding.Consultor.Enderecos.Where(e => !string.IsNullOrEmpty(e.Cep)).Select(item => new
                        {
                            tipo_endereco = item.TipoEndereco,
                            rua = item.Logradouro,
                            numero = item.Numero,
                            complemento = item.Complemento,
                            bairro = item.Bairro,
                            cidade = item.Cidade,
                            sigla_estado = item.Estado,
                            cep = item.Cep.Replace("-", ""),
                            principal = item.Principal
                        }),
                        dados_bancarios = new List<object>
                        {
                            new
                            {
                                codigo_banco = onboarding.Consultor.DadosBancarios.CodigoBanco,
                                agencia = onboarding.Consultor.DadosBancarios.Agencia,
                                conta = $"{onboarding.Consultor.DadosBancarios.ContaSemDigito}{onboarding.Consultor.DadosBancarios.Digito}",
                                codigo_operacao = 1,
                                cpf_favorecido = onboarding.Consultor.Representante.Cpf,
                                favorecido_fisica_juridica = "J",
                                principal = 1
                            }
                        }
                    }
                });
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

            onboarding.StatusOnboarding = StatusOnboarding.Finalizado;

            _onboardingRepository.Update(onboarding);

            if (Commit())
            {
            }

            return await Task.FromResult(true);
        }
    }
}