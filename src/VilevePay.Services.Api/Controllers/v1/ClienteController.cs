using System.Collections.Generic;
using System.Linq;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VilevePay.Application.Interfaces;
using VilevePay.Application.ViewModels.v1.Cliente;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;

namespace VilevePay.Services.Api.Controllers.v1
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

        [HttpGet("pessoas-fisicas/{codigoConvite}/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult ValidarPessoaFisica(string codigoConvite, [FromHeader] string cpf)
        {
            _clienteAppService.ValidarPessoaFisica(codigoConvite, cpf);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("pessoas-fisicas/{codigoConvite}/comprovantes")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult RegistrarComprovantePessoaFisica(string codigoConvite, [FromHeader] string comprovanteBase64)
        {
            _clienteAppService.RegistrarComprovantePessoaFisica(codigoConvite, comprovanteBase64);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("pessoas-juridicas/{codigoConvite}/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult ValidarPessoaJuridica(string codigoConvite, [FromHeader] string cnpj)
        {
            _clienteAppService.ValidarPessoaJuridica(codigoConvite, cnpj);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("pessoas-juridicas/{codigoConvite}/comprovantes")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult RegistrarComprovantePessoaJuridica(string codigoConvite, [FromHeader] string comprovanteBase64)
        {
            _clienteAppService.RegistrarComprovantePessoaJuridica(codigoConvite, comprovanteBase64);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("{codigoConvite}/enderecos")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult RegistrarEndereco(string codigoConvite, [FromBody] RegistrarEnderecoViewModel registrarEndereco)
        {
            _clienteAppService.RegistrarEndereco(codigoConvite, registrarEndereco.Cep, registrarEndereco.Logradouro, registrarEndereco.Numero, registrarEndereco.Complemento, registrarEndereco.Bairro, registrarEndereco.Cidade, registrarEndereco.Estado);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("{codigoConvite}/enderecos/comprovantes")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult RegistrarComprovanteEndereco(string codigoConvite, [FromHeader] string comprovanteEnderecoBase64)
        {
            _clienteAppService.RegistrarComprovanteEndereco(codigoConvite, comprovanteEnderecoBase64);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }
    }
}