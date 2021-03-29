using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Vileve.Domain.Commands.Cliente;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Notifications;
using Vileve.Domain.Interfaces;
using Vileve.Domain.Models;
using Vileve.Domain.Responses;
using Vileve.Infra.CrossCutting.Io.Http;

namespace Vileve.Domain.CommandHandlers
{
    public class ClienteCommandHandler : CommandHandler,
        IRequestHandler<ObterClientePorIdCommand, object>,
        IRequestHandler<CadastrarClienteCommand, object>,
        IRequestHandler<ObterProdutoCommand, object>,
        IRequestHandler<CadastrarProdutoCommand, bool>,
        IRequestHandler<CadastrarEnderecoCommand, bool>,
        IRequestHandler<ObterDependenteCommand, object>,
        IRequestHandler<ObterDependentePorIdCommand, object>,
        IRequestHandler<CadastrarDependenteCommand, bool>,
        IRequestHandler<AtualizarDependenteCommand, bool>,
        IRequestHandler<DeletarDependenteCommand, bool>,
        IRequestHandler<ContratarProdutoCommand, object>,
        IRequestHandler<CadastrarPagamentoCommand, bool>
    {
        private readonly ServiceManager _serviceManager;
        private readonly IHttpAppService _httpAppService;
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteProdutoRepository _clienteProdutoRepository;
        private readonly IClienteEnderecoRepository _clienteEnderecoRepository;
        private readonly IClienteDependenteRepository _clienteDependenteRepository;
        private readonly ILogger<ClienteCommandHandler> _logger;

        public ClienteCommandHandler(
            IOptions<ServiceManager> serviceManager,
            IHttpAppService httpAppService,
            IClienteRepository clienteRepository,
            IClienteProdutoRepository clienteProdutoRepository,
            IClienteEnderecoRepository clienteEnderecoRepository,
            IClienteDependenteRepository clienteDependenteRepository,
            ILogger<ClienteCommandHandler> logger,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _serviceManager = serviceManager.Value;
            _httpAppService = httpAppService;
            _clienteRepository = clienteRepository;
            _clienteProdutoRepository = clienteProdutoRepository;
            _clienteEnderecoRepository = clienteEnderecoRepository;
            _clienteDependenteRepository = clienteDependenteRepository;
            _logger = logger;
        }

        public async Task<object> Handle(ObterClientePorIdCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            var cliente = _clienteRepository.GetById(message.ClienteId);
            if (cliente == null)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Cliente não cadastrado.", message));
                return await Task.FromResult(false);
            }

            return await Task.FromResult(cliente);
        }

        public async Task<object> Handle(CadastrarClienteCommand message, CancellationToken cancellationToken)
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
                    numero_telefone = message.TelefoneCelular
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

            var cliente = new Cliente(Guid.NewGuid(), message.Cpf, message.NomeCompleto, message.DataNascimento, message.Email,
                message.TelefoneFixo, message.TelefoneCelular, message.ConsultorId);

            _clienteRepository.Add(cliente);

            if (Commit())
            {
            }

            return await Task.FromResult(cliente);
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
                var client = _httpAppService.CreateClient(_serviceManager.UrlVileve);
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

            var cliente = _clienteRepository.GetById(message.ClienteId);
            if (cliente == null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Cliente não cadastrado.", message));
                return Task.FromResult(false);
            }

            var clienteProduto = new ClienteProduto(Guid.NewGuid(), message.CodigoProdutoItem, message.ClienteId);

            _clienteProdutoRepository.Add(clienteProduto);

            if (Commit())
            {
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

            var cliente = _clienteRepository.GetById(message.ClienteId);
            if (cliente == null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Cliente não cadastrado.", message));
                return Task.FromResult(false);
            }

            var clienteEndereco = new ClienteEndereco(Guid.NewGuid(), message.Cep, message.Logradouro, message.Numero, message.Complemento,
                message.Bairro, message.Cidade, message.Estado, message.ComprovanteBase64, message.ClienteId);

            _clienteEnderecoRepository.Add(clienteEndereco);

            if (Commit())
            {
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

            var cliente = _clienteRepository.GetById(message.ClienteId);
            if (cliente == null)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Cliente não cadastrado.", message));
                return await Task.FromResult(false);
            }

            return await Task.FromResult(cliente.Dependentes);
        }

        public async Task<object> Handle(ObterDependentePorIdCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            var cliente = _clienteRepository.GetById(message.ClienteId);
            if (cliente == null)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Cliente não cadastrado.", message));
                return await Task.FromResult(false);
            }

            var clienteDependente = _clienteDependenteRepository.GetById(message.DependenteId);
            if (clienteDependente != null)
                return await Task.FromResult(clienteDependente);

            await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Dependente não encontrado.", message));
            return await Task.FromResult(false);
        }

        public Task<bool> Handle(CadastrarDependenteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var cliente = _clienteRepository.GetById(message.ClienteId);
            if (cliente == null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Cliente não cadastrado.", message));
                return Task.FromResult(false);
            }

            var clienteDependente = new ClienteDependente(Guid.NewGuid(), message.CodigoParentesco, message.NomeCompleto, message.DataNascimento, message.Cpf,
                message.Email, message.TelefoneCelular, message.Cep, message.Logradouro, message.Numero,
                message.Complemento, message.Bairro, message.Cidade, message.Estado, message.ClienteId);

            _clienteDependenteRepository.Add(clienteDependente);

            if (Commit())
            {
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(AtualizarDependenteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var cliente = _clienteRepository.GetById(message.ClienteId);
            if (cliente == null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Cliente não cadastrado.", message));
                return Task.FromResult(false);
            }

            var clienteDependente = _clienteDependenteRepository.GetById(message.DependenteId);
            if (clienteDependente != null)
            {
                clienteDependente.Update(message.CodigoParentesco, message.NomeCompleto, message.DataNascimento, message.Cpf,
                    message.Email, message.TelefoneCelular, message.Cep, message.Logradouro, message.Numero,
                    message.Complemento, message.Bairro, message.Cidade, message.Estado);

                _clienteDependenteRepository.Update(clienteDependente);

                if (Commit())
                {
                }

                return Task.FromResult(true);
            }

            _bus.RaiseEvent(new DomainNotification(message.MessageType, "Dependente não encontrado.", message));
            return Task.FromResult(false);
        }

        public Task<bool> Handle(DeletarDependenteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var cliente = _clienteRepository.GetById(message.ClienteId);
            if (cliente == null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Cliente não cadastrado.", message));
                return Task.FromResult(false);
            }

            var clienteDependente = _clienteDependenteRepository.GetById(message.DependenteId);
            if (clienteDependente != null)
            {
                _clienteDependenteRepository.Remove(message.DependenteId);

                if (Commit())
                {
                }

                return Task.FromResult(true);
            }

            _bus.RaiseEvent(new DomainNotification(message.MessageType, "Dependente não encontrado.", message));
            return Task.FromResult(false);
        }

        public async Task<object> Handle(ContratarProdutoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
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
    }
}