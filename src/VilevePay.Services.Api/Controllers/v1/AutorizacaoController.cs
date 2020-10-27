using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VilevePay.Application.Interfaces;
using VilevePay.Application.ViewModels.v1.Autorizacao;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;

namespace VilevePay.Services.Api.Controllers.v1
{
    // [Authorize]
    [ApiController]
    [Route("api/v1/autorizacoes")]
    [Produces("application/json")]
    public class AutorizacaoController : ApiController
    {
        private readonly IAutorizacaoAppService _autorizacaoAppService;
        private readonly DomainNotificationHandler _notifications;

        public AutorizacaoController(
            IAutorizacaoAppService autorizacaoAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
            : base(notifications, mediator)
        {
            _autorizacaoAppService = autorizacaoAppService;
            _notifications = (DomainNotificationHandler)notifications;
        }

        [HttpGet("login")]
        [ProducesResponseType(typeof(TokenViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromHeader] string email, [FromHeader] string senha)
        {
            var response = await _autorizacaoAppService.Login(email, senha);

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("convites/{codigoConvite}/senhas")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarSenha(string codigoConvite, [FromHeader] string email, [FromHeader] string senha, [FromHeader] string confirmarSenha)
        {
            _autorizacaoAppService.CadastrarSenha(codigoConvite, email, senha, confirmarSenha);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("convites/{codigoConvite}/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult ValidarCodigoConvite(string codigoConvite)
        {
            _autorizacaoAppService.ValidarCodigoConvite(codigoConvite);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("convites/{codigoConvite}/sms/token/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult ValidarTokenSms(string codigoConvite, [FromHeader] string numeroCelular, [FromHeader] string codigoToken)
        {
            _autorizacaoAppService.ValidarTokenSms(codigoConvite, numeroCelular, codigoToken);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("convites/{codigoConvite}/sms/token")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult EnviarTokenSms(string codigoConvite, [FromHeader] string numeroCelular)
        {
            _autorizacaoAppService.EnviarTokenSms(codigoConvite, numeroCelular);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("convites/{codigoConvite}/e-mails/token/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult ValidarTokenEmail(string codigoConvite, [FromHeader] string email, [FromHeader] string codigoToken)
        {
            _autorizacaoAppService.ValidarTokenEmail(codigoConvite, email, codigoToken);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("convites/{codigoConvite}/e-mails/token")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult EnviarTokenEmail(string codigoConvite, [FromHeader] string email)
        {
            _autorizacaoAppService.EnviarTokenEmail(codigoConvite, email);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("convites/{codigoConvite}/selfie/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult ValidarSelfie(string codigoConvite, [FromBody] SelfieViewModel selfie)
        {
            _autorizacaoAppService.ValidarSelfie(codigoConvite, selfie.FotoBase64);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }
    }
}