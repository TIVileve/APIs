using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vileve.Application.Interfaces;
using Vileve.Application.ViewModels.v1.Cliente;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Notifications;

namespace Vileve.Services.Api.Controllers.v1
{
    // [Authorize]
    [ApiController]
    [Route("api/v1/clientes")]
    [Produces("application/json")]
    public class ClienteController : ApiController
    {
        private readonly IClienteAppService _clienteAppService;

        public ClienteController(
            IClienteAppService clienteAppService,
            ILogger<ClienteController> logger,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
            : base(logger, notifications, mediator)
        {
            _clienteAppService = clienteAppService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClienteViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CadastrarCliente([FromBody] CadastrarClienteViewModel cliente)
        {
            var response = await _clienteAppService.CadastrarCliente(cliente.Cpf, cliente.NomeCompleto, cliente.DataNascimento, cliente.Email,
                cliente.TelefoneFixo, cliente.TelefoneCelular, null);

            return Response(response);
        }

        [HttpGet("seguros/produtos")]
        [ProducesResponseType(typeof(ProdutoViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterProduto()
        {
            var response = await _clienteAppService.ObterProduto();

            return Response(response);
        }

        [HttpPost("{clienteId}/seguros/produtos")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarProduto(Guid clienteId, [FromBody] CadastrarProdutoViewModel produto)
        {
            _clienteAppService.CadastrarProduto(clienteId, produto.CodigoProduto);

            return Response();
        }

        [HttpPost("{clienteId}/enderecos")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarEndereco(Guid clienteId, [FromBody] CadastrarEnderecoViewModel endereco)
        {
            _clienteAppService.CadastrarEndereco(clienteId, endereco.Cep, endereco.Logradouro, endereco.Numero, endereco.Complemento,
                endereco.Bairro, endereco.Cidade, endereco.Estado, null);

            return Response();
        }

        [HttpGet("{clienteId}/dependentes")]
        [ProducesResponseType(typeof(IEnumerable<DependenteViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterDependente(Guid clienteId)
        {
            var response = await _clienteAppService.ObterDependente(clienteId);

            return Response(response);
        }

        [HttpGet("{clienteId}/dependentes/{dependenteId}")]
        [ProducesResponseType(typeof(DependenteViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterDependentePorId(Guid clienteId, Guid dependenteId)
        {
            var response = await _clienteAppService.ObterDependentePorId(clienteId, dependenteId);

            return Response(response);
        }

        [HttpPost("{clienteId}/dependentes")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarDependente(Guid clienteId, [FromBody] CadastrarDependenteViewModel dependente)
        {
            _clienteAppService.CadastrarDependente(clienteId, dependente.CodigoParentesco, dependente.NomeCompleto, dependente.DataNascimento, dependente.Cpf,
                dependente.Email, dependente.TelefoneCelular, dependente.Endereco.Cep, dependente.Endereco.Logradouro, dependente.Endereco.Numero,
                dependente.Endereco.Complemento, dependente.Endereco.Bairro, dependente.Endereco.Cidade, dependente.Endereco.Estado);

            return Response();
        }

        [HttpPut("{clienteId}/dependentes/{dependenteId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult AtualizarDependente(Guid clienteId, Guid dependenteId, [FromBody] AtualizarDependenteViewModel dependente)
        {
            _clienteAppService.AtualizarDependente(clienteId, dependenteId, dependente.CodigoParentesco, dependente.NomeCompleto, dependente.DataNascimento, dependente.Cpf,
                dependente.Email, dependente.TelefoneCelular, dependente.Endereco.Cep, dependente.Endereco.Logradouro, dependente.Endereco.Numero,
                dependente.Endereco.Complemento, dependente.Endereco.Bairro, dependente.Endereco.Cidade, dependente.Endereco.Estado);

            return Response();
        }

        [HttpDelete("{clienteId}/dependentes/{dependenteId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeletarDependente(Guid clienteId, Guid dependenteId)
        {
            _clienteAppService.DeletarDependente(clienteId, dependenteId);

            return Response();
        }

        [HttpPost("{clienteId}/pagamentos")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarPagamento(Guid clienteId, [FromBody] CadastrarPagamentoViewModel pagamento)
        {
            _clienteAppService.CadastrarPagamento(clienteId);

            return Response();
        }

        [HttpGet("{clienteId}/calculos-mensais")]
        [ProducesResponseType(typeof(CalculoMensalViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterCalculoMensal(Guid clienteId)
        {
            var response = await _clienteAppService.ObterCalculoMensal(clienteId);

            return Response(response);
        }
    }
}