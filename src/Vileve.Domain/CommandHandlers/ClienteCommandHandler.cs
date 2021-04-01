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
using Vileve.Domain.Commands.Cliente;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Notifications;
using Vileve.Domain.Interfaces;
using Vileve.Domain.Models;
using Vileve.Domain.Responses;
using Vileve.Infra.CrossCutting.Io.Http;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Vileve.Domain.CommandHandlers
{
    public class ClienteCommandHandler : CommandHandler,
        IRequestHandler<ObterClientePorIdCommand, object>,
        IRequestHandler<CadastrarClienteCommand, object>,
        IRequestHandler<AtualizarClienteCommand, bool>,
        IRequestHandler<ObterProdutoCommand, object>,
        IRequestHandler<CadastrarProdutoCommand, bool>,
        IRequestHandler<CadastrarEnderecoCommand, bool>,
        IRequestHandler<AtualizarEnderecoCommand, bool>,
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
        private readonly IClienteFontePagadoraRepository _clienteFontePagadoraRepository;
        private readonly IClienteProdutoRepository _clienteProdutoRepository;
        private readonly IClienteEnderecoRepository _clienteEnderecoRepository;
        private readonly IClienteDependenteRepository _clienteDependenteRepository;
        private readonly IUser _user;
        private readonly ILogger<ClienteCommandHandler> _logger;

        public ClienteCommandHandler(
            IOptions<ServiceManager> serviceManager,
            IHttpAppService httpAppService,
            IClienteRepository clienteRepository,
            IClienteFontePagadoraRepository clienteFontePagadoraRepository,
            IClienteProdutoRepository clienteProdutoRepository,
            IClienteEnderecoRepository clienteEnderecoRepository,
            IClienteDependenteRepository clienteDependenteRepository,
            IUser user,
            ILogger<ClienteCommandHandler> logger,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _serviceManager = serviceManager.Value;
            _httpAppService = httpAppService;
            _clienteRepository = clienteRepository;
            _clienteFontePagadoraRepository = clienteFontePagadoraRepository;
            _clienteProdutoRepository = clienteProdutoRepository;
            _clienteEnderecoRepository = clienteEnderecoRepository;
            _clienteDependenteRepository = clienteDependenteRepository;
            _user = user;
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

            var clienteFontePagadora = new ClienteFontePagadora(Guid.NewGuid(), message.InssNumeroBeneficio, message.InssSalario, message.InssEspecie, message.OutrosDiaPagamento,
                cliente.Id);

            _clienteFontePagadoraRepository.Add(clienteFontePagadora);

            if (Commit())
            {
            }

            return await Task.FromResult(cliente);
        }

        public Task<bool> Handle(AtualizarClienteCommand message, CancellationToken cancellationToken)
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

            cliente.Update(message.Cpf, message.NomeCompleto, message.DataNascimento, message.Email,
                message.TelefoneFixo, message.TelefoneCelular, message.ConsultorId);

            _clienteRepository.Update(cliente);

            var clienteFontePagadora = cliente.FontePagadora?.Update(message.InssNumeroBeneficio, message.InssSalario, message.InssEspecie, message.OutrosDiaPagamento);

            if (clienteFontePagadora != null)
                _clienteFontePagadoraRepository.Update(clienteFontePagadora);

            if (Commit())
            {
            }

            return Task.FromResult(true);
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

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _user.Token);

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

        public Task<bool> Handle(AtualizarEnderecoCommand message, CancellationToken cancellationToken)
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

            var clienteEndereco = _clienteEnderecoRepository.GetById(message.EnderecoId);
            if (clienteEndereco != null)
            {
                clienteEndereco.Update(message.Cep, message.Logradouro, message.Numero, message.Complemento,
                    message.Bairro, message.Cidade, message.Estado, message.ComprovanteBase64);

                _clienteEnderecoRepository.Update(clienteEndereco);

                if (Commit())
                {
                }

                return Task.FromResult(true);
            }

            _bus.RaiseEvent(new DomainNotification(message.MessageType, "Endereço não encontrado.", message));
            return Task.FromResult(false);
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

            var cliente = _clienteRepository.GetById(message.ClienteId);
            if (cliente == null)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Cliente não cadastrado.", message));
                return await Task.FromResult(false);
            }

            try
            {
                var client = _httpAppService.CreateClient(_serviceManager.UrlVileve);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _user.Token);

                string apelido;
                string nomeSocial;
                var telefones = new List<object>();

                if (cliente.NomeCompleto.IndexOf(" ", StringComparison.Ordinal).Equals(-1))
                {
                    apelido = cliente.NomeCompleto;
                    nomeSocial = cliente.NomeCompleto;
                }
                else
                {
                    apelido = cliente.NomeCompleto.Substring(0, cliente.NomeCompleto.IndexOf(" ", StringComparison.Ordinal));
                    nomeSocial = cliente.NomeCompleto.Substring(0, cliente.NomeCompleto.IndexOf(" ", StringComparison.Ordinal));
                }

                if (!string.IsNullOrWhiteSpace(cliente.TelefoneFixo))
                {
                    telefones.Add(new
                    {
                        tipo_telefone = 1,
                        ddd = cliente.TelefoneFixo.Replace("+55", "").Substring(0, 2),
                        telefone = cliente.TelefoneFixo.Replace("+55", "").Substring(2),
                        principal = 1
                    });
                }

                if (!string.IsNullOrWhiteSpace(cliente.TelefoneCelular))
                {
                    telefones.Add(new
                    {
                        tipo_telefone = 2,
                        ddd = cliente.TelefoneCelular.Replace("+55", "").Substring(0, 2),
                        telefone = cliente.TelefoneCelular.Replace("+55", "").Substring(2),
                        principal = 0
                    });
                }

                var contratarProduto = await _httpAppService.OnPost<ContratarProduto, object>(client, message.RequestId, "v1/proposta/nova-contratacao", new
                {
                    codigo_produto_item = cliente.Produto.CodigoProdutoItem,
                    fonte_pagadora = new
                    {
                        // inss = new
                        // {
                        //     numero_beneficio = 0,
                        //     salario = 0,
                        //     especie = 0
                        // },
                        outros = new
                        {
                            dia_pagamento = 30
                        }
                    },
                    pessoa = new
                    {
                        // codigo_consultor = 0,
                        nome_completo = cliente.NomeCompleto,
                        apelido,
                        nome_social = nomeSocial,
                        codigo_sexo = 1,
                        codigo_estado_civil = 1,
                        data_nascimento = cliente.DataNascimento.ToString("yyyy-MM-dd"),
                        codigo_nacionalidade = 39,
                        cpf = cliente.Cpf,
                        emails = new List<object>
                        {
                            new
                            {
                                tipo_email = 1,
                                email = cliente.Email,
                                principal = 1
                            }
                        },
                        telefones,
                        enderecos = cliente.Enderecos.Select(item => new
                        {
                            tipo_endereco = 1,
                            rua = item.Logradouro,
                            numero = item.Numero,
                            complemento = item.Complemento,
                            bairro = item.Bairro,
                            cidade = item.Cidade,
                            sigla_estado = item.Estado,
                            cep = item.Cep.Replace("-", ""),
                            principal = 1
                        }),
                        // documentos = new
                        // {
                        //     rg = new
                        //     {
                        //         registro_identidade = "string",
                        //         uf_expedicao = "string",
                        //         orgao_emissor = "string",
                        //         data_emissao = "yyyy-mm-dd"
                        //     },
                        //     titulo_eleitor = new
                        //     {
                        //         numero_inscricao = 0,
                        //         data_emissao = "yyyy-mm-dd",
                        //         zona = "string",
                        //         secao = "string"
                        //     },
                        //     carteira_trabalho = new
                        //     {
                        //         numero = 0,
                        //         serie = 0,
                        //         numero_nit = 0,
                        //         uf_expedicao = "string",
                        //         data_emissao = "yyyy-mm-dd"
                        //     },
                        //     carteira_habilitacao = new
                        //     {
                        //         numero = 0,
                        //         tipo = "string",
                        //         data_vencimento = "yyyy-mm-dd"
                        //     },
                        //     certidao_servico_militar = new
                        //     {
                        //         numero = 0,
                        //         categoria = "string",
                        //         numero_beneficio = 0
                        //     }
                        // }
                    },
                    dependentes = cliente.Dependentes.Select(item => new
                    {
                        codigo_tipo_dependente = item.CodigoParentesco,
                        nome_completo = item.NomeCompleto,
                        data_nascimento = item.DataNascimento.ToString("yyyy-MM-dd"),
                        cpf = item.Cpf,
                        email = item.Email,
                        celular = item.TelefoneCelular,
                        cep = item.Cep.Replace("-", ""),
                        rua = item.Logradouro,
                        numero = item.Numero,
                        complemento = item.Complemento,
                        bairro = item.Bairro,
                        cidade = item.Cidade,
                        estado = item.Estado
                    })
                });

                return await Task.FromResult(contratarProduto);
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