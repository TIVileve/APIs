﻿using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using MediatR;
using Microsoft.Extensions.Logging;
using Vileve.Domain.Commands.Consultor;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Notifications;
using Vileve.Domain.Enums;
using Vileve.Domain.Interfaces;
using Vileve.Domain.Models;
using Vileve.Domain.Responses;
using Vileve.Infra.CrossCutting.Io.Http;

namespace Vileve.Domain.CommandHandlers
{
    public class ConsultorCommandHandler : CommandHandler,
        IRequestHandler<ObterEnderecoCommand, object>,
        IRequestHandler<ObterEnderecoPorIdCommand, object>,
        IRequestHandler<CadastrarEnderecoCommand, bool>,
        IRequestHandler<DeletarEnderecoCommand, bool>,
        IRequestHandler<CadastrarPessoaJuridicaCommand, bool>,
        IRequestHandler<CadastrarRepresentanteCommand, bool>,
        IRequestHandler<ObterStatusOnboardingCommand, object>,
        IRequestHandler<ValidarConsultorCommand, bool>,
        IRequestHandler<ValidarPessoaJuridicaCommand, bool>,
        IRequestHandler<ValidarRepresentanteCommand, bool>
    {
        private readonly IHttpAppService _httpAppService;
        private readonly IOnboardingRepository _onboardingRepository;
        private readonly IConsultorRepository _consultorRepository;
        private readonly IDadosBancariosRepository _dadosBancariosRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IRepresentanteRepository _representanteRepository;
        private readonly IRepresentanteEmailRepository _representanteEmailRepository;
        private readonly IRepresentanteTelefoneRepository _representanteTelefoneRepository;
        private readonly ILogger<ConsultorCommandHandler> _logger;

        public ConsultorCommandHandler(
            IHttpAppService httpAppService,
            IOnboardingRepository onboardingRepository,
            IConsultorRepository consultorRepository,
            IDadosBancariosRepository dadosBancariosRepository,
            IEnderecoRepository enderecoRepository,
            IRepresentanteRepository representanteRepository,
            IRepresentanteEmailRepository representanteEmailRepository,
            IRepresentanteTelefoneRepository representanteTelefoneRepository,
            ILogger<ConsultorCommandHandler> logger,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _httpAppService = httpAppService;
            _onboardingRepository = onboardingRepository;
            _consultorRepository = consultorRepository;
            _dadosBancariosRepository = dadosBancariosRepository;
            _enderecoRepository = enderecoRepository;
            _representanteRepository = representanteRepository;
            _representanteEmailRepository = representanteEmailRepository;
            _representanteTelefoneRepository = representanteTelefoneRepository;
            _logger = logger;
        }

        public async Task<object> Handle(ObterEnderecoCommand message, CancellationToken cancellationToken)
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

            if (onboarding.Consultor == null)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Consultor não cadastrado.", message));
                return await Task.FromResult(false);
            }

            var enderecos = _enderecoRepository.Find(e => e.Consultor.Id.Equals(onboarding.Consultor.Id)).OrderByDescending(e => e.Principal).ToList();

            return await Task.FromResult(enderecos);
        }

        public async Task<object> Handle(ObterEnderecoPorIdCommand message, CancellationToken cancellationToken)
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

            if (onboarding.Consultor == null)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Consultor não cadastrado.", message));
                return await Task.FromResult(false);
            }

            var endereco = _enderecoRepository.GetById(message.EnderecoId);
            if (endereco != null)
                return await Task.FromResult(endereco);

            await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Endereço não encontrado.", message));
            return await Task.FromResult(false);
        }

        public Task<bool> Handle(CadastrarEnderecoCommand message, CancellationToken cancellationToken)
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

            if (onboarding.Consultor == null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Consultor não cadastrado.", message));
                return Task.FromResult(false);
            }

            if (message.Principal)
                foreach (var item in onboarding.Consultor.Enderecos.Where(e => e.TipoEndereco.Equals((TipoEndereco)message.TipoEndereco)))
                    item.Principal = false;

            var endereco = new Endereco(Guid.NewGuid(), (TipoEndereco)message.TipoEndereco, message.Cep, message.Logradouro, message.Numero,
                message.Complemento, message.Bairro, message.Cidade, message.Estado, message.Principal,
                message.ComprovanteBase64, onboarding.Consultor.Id);

            _enderecoRepository.Add(endereco);

            onboarding.StatusOnboarding = ((TipoEndereco)message.TipoEndereco).Equals(TipoEndereco.Consultor) ? StatusOnboarding.EnderecoCnpj : StatusOnboarding.EnderecoRepresentante;

            _onboardingRepository.Update(onboarding);

            if (Commit())
            {
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(DeletarEnderecoCommand message, CancellationToken cancellationToken)
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

            if (onboarding.Consultor == null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Consultor não cadastrado.", message));
                return Task.FromResult(false);
            }

            var endereco = _enderecoRepository.GetById(message.EnderecoId);
            if (endereco != null)
            {
                _enderecoRepository.Remove(message.EnderecoId);

                if (Commit())
                {
                }

                return Task.FromResult(true);
            }

            _bus.RaiseEvent(new DomainNotification(message.MessageType, "Endereço não encontrado.", message));
            return Task.FromResult(false);
        }

        public Task<bool> Handle(CadastrarPessoaJuridicaCommand message, CancellationToken cancellationToken)
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

            if (onboarding.Consultor != null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "O consultor já possui cadastro no sistema.", message));
                return Task.FromResult(false);
            }

            var consultor = new Consultor(Guid.NewGuid(), message.Cnpj, message.RazaoSocial, message.NomeFantasia, message.InscricaoMunicipal,
                message.InscricaoEstadual, message.ContratoSocialBase64, message.UltimaAlteracaoBase64, onboarding.Id);

            _consultorRepository.Add(consultor);

            var dadosBancarios = new DadosBancarios(Guid.NewGuid(), message.CodigoBanco, message.Agencia, message.ContaSemDigito, message.Digito,
                message.TipoConta.ToString(), consultor.Id);

            _dadosBancariosRepository.Add(dadosBancarios);

            onboarding.StatusOnboarding = StatusOnboarding.ContratoSocial;

            _onboardingRepository.Update(onboarding);

            if (Commit())
            {
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(CadastrarRepresentanteCommand message, CancellationToken cancellationToken)
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

            if (onboarding.Consultor == null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Consultor não cadastrado.", message));
                return Task.FromResult(false);
            }

            var representante = new Representante(Guid.NewGuid(), message.Cpf, message.NomeCompleto, message.Sexo, message.EstadoCivil,
                message.Nacionalidade, message.DocumentoFrenteBase64, message.DocumentoVersoBase64, onboarding.Consultor.Id);

            _representanteRepository.Add(representante);

            foreach (var value in message.Emails)
            {
                var tipoEmail = (int?)value.GetType().GetProperty("TipoEmail")?.GetValue(value, null);
                var email = value.GetType().GetProperty("Email")?.GetValue(value, null)?.ToString();

                var representanteEmail = new RepresentanteEmail(Guid.NewGuid(), tipoEmail.GetValueOrDefault(), email, representante.Id);

                _representanteEmailRepository.Add(representanteEmail);
            }

            foreach (var value in message.Telefones)
            {
                var tipoTelefone = (int?)value.GetType().GetProperty("TipoTelefone")?.GetValue(value, null);
                var numero = value.GetType().GetProperty("Numero")?.GetValue(value, null)?.ToString();

                var representanteTelefone = new RepresentanteTelefone(Guid.NewGuid(), tipoTelefone.GetValueOrDefault(), numero, representante.Id);

                _representanteTelefoneRepository.Add(representanteTelefone);
            }

            onboarding.StatusOnboarding = StatusOnboarding.DadosRepresentante;

            _onboardingRepository.Update(onboarding);

            if (Commit())
            {
            }

            return Task.FromResult(true);
        }

        public async Task<object> Handle(ObterStatusOnboardingCommand message, CancellationToken cancellationToken)
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

            return await Task.FromResult(onboarding);
        }

        public async Task<bool> Handle(ValidarConsultorCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
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

                var verificaUsuario = await _httpAppService.OnGet<VerificaUsuario>(client, message.RequestId, $"v1/auth/verifica-usuario/{HttpUtility.UrlEncode(message.Email)}");
                if (verificaUsuario.Valido.Equals(true))
                {
                    await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Consultor não encontrado.", message));
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

        public async Task<bool> Handle(ValidarPessoaJuridicaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
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

                try
                {
                    await _httpAppService.OnGet<object>(client, message.RequestId, $"v1/consultor/consultar/{message.Cnpj}");
                }
                catch (Exception)
                {
                    await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Pessoa jurídica não encontrada.", message));
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

        public async Task<bool> Handle(ValidarRepresentanteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
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

                try
                {
                    await _httpAppService.OnGet<object>(client, message.RequestId, $"v1/consultor/consultar/{message.Cpf}");
                }
                catch (Exception)
                {
                    await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Representante não encontrado.", message));
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
    }
}