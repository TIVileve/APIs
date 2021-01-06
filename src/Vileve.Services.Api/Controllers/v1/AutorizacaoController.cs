using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vileve.Application.Interfaces;
using Vileve.Application.ViewModels.v1.Autorizacao;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Notifications;

namespace Vileve.Services.Api.Controllers.v1
{
    // [Authorize]
    [ApiController]
    [Route("api/v1/autorizacoes")]
    [Produces("application/json")]
    public class AutorizacaoController : ApiController
    {
        private readonly IAutorizacaoAppService _autorizacaoAppService;
        private readonly ILogger<AutorizacaoController> _logger;
        private readonly DomainNotificationHandler _notifications;

        public AutorizacaoController(
            IAutorizacaoAppService autorizacaoAppService,
            ILogger<AutorizacaoController> logger,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
            : base(notifications, mediator)
        {
            _autorizacaoAppService = autorizacaoAppService;
            _logger = logger;
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

            if (_notifications.HasNotifications())
            {
                _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
                {
                    parameters = new { email, senha },
                    errors = _notifications.GetNotifications().Select(n => n)
                }));
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("sms/token/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ValidarTokenSms([FromHeader] string numeroCelular, [FromHeader] string codigoToken)
        {
            await _autorizacaoAppService.ValidarTokenSms("******", numeroCelular, codigoToken);

            if (IsValidOperation())
            {
                return NoContent();
            }

            if (_notifications.HasNotifications())
            {
                _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
                {
                    parameters = new { numeroCelular, codigoToken },
                    errors = _notifications.GetNotifications().Select(n => n)
                }));
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("sms/token")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EnviarTokenSms([FromHeader] string numeroCelular)
        {
            await _autorizacaoAppService.EnviarTokenSms(numeroCelular);

            if (IsValidOperation())
            {
                return NoContent();
            }

            if (_notifications.HasNotifications())
            {
                _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
                {
                    parameters = new { numeroCelular },
                    errors = _notifications.GetNotifications().Select(n => n)
                }));
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("e-mails/token")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EnviarTokenEmail([FromHeader] string email)
        {
            await _autorizacaoAppService.EnviarTokenEmail(email);

            if (IsValidOperation())
            {
                return NoContent();
            }

            if (_notifications.HasNotifications())
            {
                _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
                {
                    parameters = new { email },
                    errors = _notifications.GetNotifications().Select(n => n)
                }));
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("convites/{codigoConvite}/senhas")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarSenha(string codigoConvite, [FromHeader] string numeroCelular, [FromHeader] string email, [FromHeader] string senha,
            [FromHeader] string confirmarSenha)
        {
            _autorizacaoAppService.CadastrarSenha(codigoConvite, numeroCelular, email, senha,
                confirmarSenha);

            if (IsValidOperation())
            {
                return NoContent();
            }

            if (_notifications.HasNotifications())
            {
                _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
                {
                    parameters = new
                    {
                        codigoConvite,
                        numeroCelular,
                        email,
                        senha,
                        confirmarSenha
                    },
                    errors = _notifications.GetNotifications().Select(n => n)
                }));
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("convites/{codigoConvite}/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ValidarCodigoConvite(string codigoConvite)
        {
            await _autorizacaoAppService.ValidarCodigoConvite(codigoConvite);

            if (IsValidOperation())
            {
                return NoContent();
            }

            if (_notifications.HasNotifications())
            {
                _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
                {
                    parameters = new { codigoConvite },
                    errors = _notifications.GetNotifications().Select(n => n)
                }));
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("convites/{codigoConvite}/sms/token/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ValidarTokenSms(string codigoConvite, [FromHeader] string numeroCelular, [FromHeader] string codigoToken)
        {
            await _autorizacaoAppService.ValidarTokenSms(codigoConvite, numeroCelular, codigoToken);

            if (IsValidOperation())
            {
                return NoContent();
            }

            if (_notifications.HasNotifications())
            {
                _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
                {
                    parameters = new { codigoConvite, numeroCelular, codigoToken },
                    errors = _notifications.GetNotifications().Select(n => n)
                }));
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("convites/{codigoConvite}/e-mails/token/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ValidarTokenEmail(string codigoConvite, [FromHeader] string numeroCelular, [FromHeader] string email, [FromHeader] string codigoToken)
        {
            await _autorizacaoAppService.ValidarTokenEmail(codigoConvite, numeroCelular, email, codigoToken);

            if (IsValidOperation())
            {
                return NoContent();
            }

            if (_notifications.HasNotifications())
            {
                _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
                {
                    parameters = new { codigoConvite, numeroCelular, email, codigoToken },
                    errors = _notifications.GetNotifications().Select(n => n)
                }));
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("convites/{codigoConvite}/selfie/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ValidarSelfie(string codigoConvite, [FromHeader] string numeroCelular, [FromBody] ValidarSelfieViewModel selfie)
        {
            await _autorizacaoAppService.ValidarSelfie(codigoConvite, numeroCelular, selfie.FotoBase64);

            if (IsValidOperation())
            {
                return NoContent();
            }

            if (_notifications.HasNotifications())
            {
                _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
                {
                    parameters = new { codigoConvite, numeroCelular, selfie.FotoBase64 },
                    errors = _notifications.GetNotifications().Select(n => n)
                }));
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }
    }
}