using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VilevePay.Application.Interfaces;
using VilevePay.Application.ViewModels.v1.Consultor;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;

namespace VilevePay.Services.Api.Controllers.v1
{
    // [Authorize]
    [ApiController]
    [Route("api/v1/consultores")]
    [Produces("application/json")]
    public class ConsultorController : ApiController
    {
        private readonly IConsultorAppService _consultorAppService;
        private readonly DomainNotificationHandler _notifications;

        public ConsultorController(
            IConsultorAppService consultorAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
            : base(notifications, mediator)
        {
            _consultorAppService = consultorAppService;
            _notifications = (DomainNotificationHandler)notifications;
        }

        [HttpPost("convites/{codigoConvite}/arquivos")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarArquivo(string codigoConvite, [FromBody] object arquivo)
        {
            _consultorAppService.CadastrarArquivo(codigoConvite);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("convites/{codigoConvite}/dados-bancarios")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarDadosBancarios(string codigoConvite, [FromBody] object dadosBancarios)
        {
            _consultorAppService.CadastrarDadosBancarios(codigoConvite);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("convites/{codigoConvite}/documentos")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarDocumento(string codigoConvite, [FromBody] object documento)
        {
            _consultorAppService.CadastrarDocumento(codigoConvite);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("convites/{codigoConvite}/emails")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarEmail(string codigoConvite, [FromBody] object email)
        {
            _consultorAppService.CadastrarEmail(codigoConvite);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("convites/{codigoConvite}/enderecos")]
        [ProducesResponseType(typeof(IEnumerable<ConsultorEnderecoViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterEndereco(string codigoConvite)
        {
            var response = await _consultorAppService.ObterEndereco(codigoConvite);

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("convites/{codigoConvite}/enderecos")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarEndereco(string codigoConvite, [FromBody] object endereco)
        {
            _consultorAppService.CadastrarEndereco(codigoConvite);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpDelete("convites/{codigoConvite}/enderecos/{enderecoId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeletarEndereco(string codigoConvite, Guid enderecoId)
        {
            _consultorAppService.DeletarEndereco(codigoConvite, enderecoId);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("convites/{codigoConvite}/pessoas-juridicas")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarPessoaJuridica(string codigoConvite, [FromBody] object pessoaJuridica)
        {
            _consultorAppService.CadastrarPessoaJuridica(codigoConvite);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("convites/{codigoConvite}/telefones")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarTelefone(string codigoConvite, [FromBody] object telefone)
        {
            _consultorAppService.CadastrarTelefone(codigoConvite);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("convites/{codigoConvite}/onboarding/status")]
        [ProducesResponseType(typeof(StatusOnboardingViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterStatusOnboarding(string codigoConvite)
        {
            var response = await _consultorAppService.ObterStatusOnboarding(codigoConvite);

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }
    }
}