using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Vileve.Domain.Commands.Autorizacao;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Notifications;
using Vileve.Domain.Enums;
using Vileve.Domain.Interfaces;
using Vileve.Domain.Models;
using Vileve.Domain.Responses;
using Vileve.Infra.CrossCutting.Io.Http;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
        private readonly ServiceManager _serviceManager;
        private readonly IHttpAppService _httpAppService;
        private readonly IOnboardingRepository _onboardingRepository;
        private readonly ILogger<AutorizacaoCommandHandler> _logger;

        public AutorizacaoCommandHandler(
            IOptions<ServiceManager> serviceManager,
            IHttpAppService httpAppService,
            IOnboardingRepository onboardingRepository,
            ILogger<AutorizacaoCommandHandler> logger,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _serviceManager = serviceManager.Value;
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
                var client = _httpAppService.CreateClient(_serviceManager.UrlVileve);
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
                var client = _httpAppService.CreateClient(_serviceManager.UrlVileve);
                var validarToken = await _httpAppService.OnPost<ValidarToken, object>(client, message.RequestId, "v1/validacao-contato/validar-token", new
                {
                    token = message.CodigoToken,
                    numero_telefone = message.NumeroCelular
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
            {
                if (onboarding.StatusOnboarding.Equals(StatusOnboarding.ValidacaoToken) || onboarding.StatusOnboarding.Equals(StatusOnboarding.ValidacaoEmail))
                    return await Task.FromResult(true);

                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Este código do convite e número de celular estão vinculados a outro usuário.", message));
                return await Task.FromResult(false);
            }

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
                var client = _httpAppService.CreateClient(_serviceManager.UrlVileve);
                var validarToken = await _httpAppService.OnPost<ValidarToken, object>(client, message.RequestId, "v1/validacao-contato/validar-token", new
                {
                    token = message.CodigoToken,
                    email = message.Email
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
                var client = _httpAppService.CreateClient(_serviceManager.UrlVileve);
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
                var client = _httpAppService.CreateClient(_serviceManager.UrlVileve);
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
                var client = _httpAppService.CreateClient(_serviceManager.UrlVileve);

                var token = await _httpAppService.OnPost<Token, object>(client, message.RequestId, "v1/auth/login", new
                {
                    usuario = _serviceManager.UserVileve,
                    senha = _serviceManager.PasswordVileve
                });
                if (token == null || string.IsNullOrWhiteSpace(token.AccessToken))
                {
                    await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Usuário de integração não encontrado.", message));
                    return await Task.FromResult(false);
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

                string apelido;
                string nomeSocial;

                if (onboarding.Consultor.Representante.NomeCompleto.IndexOf(" ", StringComparison.Ordinal).Equals(-1))
                {
                    apelido = onboarding.Consultor.Representante.NomeCompleto;
                    nomeSocial = onboarding.Consultor.Representante.NomeCompleto;
                }
                else
                {
                    apelido = onboarding.Consultor.Representante.NomeCompleto.Substring(0, onboarding.Consultor.Representante.NomeCompleto.IndexOf(" ", StringComparison.Ordinal));
                    nomeSocial = onboarding.Consultor.Representante.NomeCompleto.Substring(0, onboarding.Consultor.Representante.NomeCompleto.IndexOf(" ", StringComparison.Ordinal));
                }

                var pessoaJuridica = await _httpAppService.OnPost<PessoaJuridica, object>(client, message.RequestId, $"v1/consultor/cadastrar/pessoajuridica/{message.CodigoConvite}", new
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
                        apelido,
                        nome_social = nomeSocial,
                        codigo_sexo = onboarding.Consultor.Representante.Sexo,
                        codigo_estado_civil = onboarding.Consultor.Representante.EstadoCivil,
                        data_nascimento = "1962-01-13",
                        codigo_nacionalidade = onboarding.Consultor.Representante.Nacionalidade,
                        cpf = onboarding.Consultor.Representante.Cpf,
                        autenticacao = new
                        {
                            usuario = onboarding.Email,
                            senha = onboarding.Senha,
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
                        enderecos = onboarding.Consultor.Enderecos.Select(item => new
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

                if (pessoaJuridica.Sucesso)
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(message.FotoBase64))
                        {
                            var selfie = await _httpAppService.OnPost<object, object>(client, message.RequestId, "v1/pessoa/envio/selfie", new
                            {
                                codigo_pessoa = pessoaJuridica.CodigoPessoa,
                                arquivo_base64 = message.FotoBase64
                            });
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.Log(LogLevel.Error, e, JsonSerializer.Serialize(new
                        {
                            message.RequestId,
                            e.Message
                        }));
                    }

                    try
                    {
                        if (!string.IsNullOrWhiteSpace(onboarding.Consultor.Representante.DocumentoFrenteBase64) &&
                            !string.IsNullOrWhiteSpace(onboarding.Consultor.Representante.DocumentoVersoBase64))
                        {
                            var documentoIdentificacao = await _httpAppService.OnPost<object, object>(client, message.RequestId, "v1/pessoa/envio/documento-identificacao", new
                            {
                                codigo_pessoa = pessoaJuridica.CodigoPessoa,
                                frente = new
                                {
                                    arquivo_base64 = onboarding.Consultor.Representante.DocumentoFrenteBase64
                                },
                                verso = new
                                {
                                    arquivo_base64 = onboarding.Consultor.Representante.DocumentoVersoBase64
                                }
                            });
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.Log(LogLevel.Error, e, JsonSerializer.Serialize(new
                        {
                            message.RequestId,
                            e.Message
                        }));
                    }

                    // try
                    // {
                    //     foreach (var item in onboarding.Consultor.Enderecos)
                    //     {
                    //         await _httpAppService.OnPost<object, object>(client, message.RequestId, "v1/pessoa/envio/comprovante-endereco", new
                    //         {
                    //             codigo_pessoa = 0,
                    //             arquivo_base64 = item.ComprovanteBase64
                    //         });
                    //     }
                    // }
                    // catch (Exception e)
                    // {
                    //     _logger.Log(LogLevel.Error, e, JsonSerializer.Serialize(new
                    //     {
                    //         message.RequestId,
                    //         e.Message
                    //     }));
                    // }

                    try
                    {
                        if (!string.IsNullOrWhiteSpace(onboarding.Consultor.ContratoSocialBase64))
                        {
                            var contratoSocial = await _httpAppService.OnPost<object, object>(client, message.RequestId, "v1/pessoa/envio/contrato-social", new
                            {
                                codigo_pessoa = pessoaJuridica.CodigoPessoa,
                                tipo_contrato = "contrato",
                                arquivo_base64 = onboarding.Consultor.ContratoSocialBase64
                            });
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.Log(LogLevel.Error, e, JsonSerializer.Serialize(new
                        {
                            message.RequestId,
                            e.Message
                        }));
                    }

                    try
                    {
                        if (!string.IsNullOrWhiteSpace(onboarding.Consultor.UltimaAlteracaoBase64))
                        {
                            var alteracaoContratoSocial = await _httpAppService.OnPost<object, object>(client, message.RequestId, "v1/pessoa/envio/contrato-social", new
                            {
                                codigo_pessoa = pessoaJuridica.CodigoPessoa,
                                tipo_contrato = "alteracao",
                                arquivo_base64 = onboarding.Consultor.UltimaAlteracaoBase64
                            });
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.Log(LogLevel.Error, e, JsonSerializer.Serialize(new
                        {
                            message.RequestId,
                            e.Message
                        }));
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e, JsonSerializer.Serialize(new
                {
                    message.RequestId,
                    e.Message
                }));

                var errors = new List<string>();

                try
                {
                    var responseError = JsonConvert.DeserializeObject<ResponseError>(e.Message);
                    errors.AddRange(responseError.Erros);
                }
                catch (Exception)
                {
                    // ignored
                }

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        await _bus.RaiseEvent(new DomainNotification(message.MessageType, error, message));
                    }
                }
                else
                {
                    await _bus.RaiseEvent(new DomainNotification(message.MessageType, "O sistema está momentaneamente indisponível, tente novamente mais tarde.", message));
                }

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