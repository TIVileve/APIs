using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        private readonly DomainNotificationHandler _notifications;

        public ClienteController(
            IClienteAppService clienteAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
            : base(notifications, mediator)
        {
            _clienteAppService = clienteAppService;
            _notifications = (DomainNotificationHandler)notifications;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClienteViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CadastrarCliente([FromBody] CadastrarClienteViewModel cliente)
        {
            var response = await _clienteAppService.CadastrarCliente();

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("seguros/produtos")]
        [ProducesResponseType(typeof(ProdutoViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterProduto()
        {
            var response = await _clienteAppService.ObterProduto();

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("{clienteId}/seguros/produtos")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarProduto(Guid clienteId, [FromBody] CadastrarProdutoViewModel produto)
        {
            _clienteAppService.CadastrarProduto(clienteId);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("{clienteId}/enderecos")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarEndereco(Guid clienteId, [FromBody] CadastrarEnderecoViewModel endereco)
        {
            _clienteAppService.CadastrarEndereco(clienteId, endereco.Cep, endereco.Logradouro, endereco.Numero, endereco.Complemento,
                endereco.Bairro, endereco.Cidade, endereco.Estado);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("{clienteId}/dependentes")]
        [ProducesResponseType(typeof(IEnumerable<DependenteViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterDependente(Guid clienteId)
        {
            var response = await _clienteAppService.ObterDependente(clienteId);

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("{clienteId}/dependentes/{dependenteId}")]
        [ProducesResponseType(typeof(DependenteViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterDependentePorId(Guid clienteId, Guid dependenteId)
        {
            var response = await _clienteAppService.ObterDependentePorId(clienteId, dependenteId);

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("{clienteId}/dependentes")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarDependente(Guid clienteId, [FromBody] CadastrarDependenteViewModel dependente)
        {
            _clienteAppService.CadastrarDependente(clienteId);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpDelete("{clienteId}/dependentes/{dependenteId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeletarDependente(Guid clienteId, Guid dependenteId)
        {
            _clienteAppService.DeletarDependente(clienteId, dependenteId);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("{clienteId}/pagamentos")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarPagamento(Guid clienteId, [FromBody] CadastrarPagamentoViewModel pagamento)
        {
            _clienteAppService.CadastrarPagamento(clienteId);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("{clienteId}/calculos-mensais")]
        [ProducesResponseType(typeof(CalculoMensalViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterCalculoMensal(Guid clienteId)
        {
            var response = await _clienteAppService.ObterCalculoMensal(clienteId);

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }
    }
}