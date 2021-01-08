using System.Collections.Generic;
using System.Net;
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

        public AutorizacaoController(
            IAutorizacaoAppService autorizacaoAppService,
            ILogger<AutorizacaoController> logger,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
            : base(logger, notifications, mediator)
        {
            _autorizacaoAppService = autorizacaoAppService;
        }

        [HttpGet("login")]
        [ProducesResponseType(typeof(TokenViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromHeader] string email, [FromHeader] string senha)
        {
            var response = await _autorizacaoAppService.Login(email, senha);

            return Response(response);
        }

        [HttpGet("sms/token/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ValidarTokenSms([FromHeader] string numeroCelular, [FromHeader] string codigoToken)
        {
            await _autorizacaoAppService.ValidarTokenSms("******", numeroCelular, codigoToken);

            return Response();
        }

        [HttpPost("sms/token")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EnviarTokenSms([FromHeader] string numeroCelular)
        {
            await _autorizacaoAppService.EnviarTokenSms(numeroCelular);

            return Response();
        }

        [HttpPost("e-mails/token")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EnviarTokenEmail([FromHeader] string email)
        {
            await _autorizacaoAppService.EnviarTokenEmail(email);

            return Response();
        }

        [HttpPost("convites/{codigoConvite}/senhas")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarSenha(string codigoConvite, [FromHeader] string numeroCelular, [FromHeader] string email, [FromHeader] string senha,
            [FromHeader] string confirmarSenha)
        {
            _autorizacaoAppService.CadastrarSenha(codigoConvite, numeroCelular, email, senha,
                confirmarSenha);

            return Response();
        }

        [HttpGet("convites/{codigoConvite}/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ValidarCodigoConvite(string codigoConvite)
        {
            await _autorizacaoAppService.ValidarCodigoConvite(codigoConvite);

            return Response();
        }

        [HttpGet("convites/{codigoConvite}/sms/token/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ValidarTokenSms(string codigoConvite, [FromHeader] string numeroCelular, [FromHeader] string codigoToken)
        {
            await _autorizacaoAppService.ValidarTokenSms(codigoConvite, numeroCelular, codigoToken);

            return Response();
        }

        [HttpGet("convites/{codigoConvite}/e-mails/token/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ValidarTokenEmail(string codigoConvite, [FromHeader] string numeroCelular, [FromHeader] string email, [FromHeader] string codigoToken)
        {
            await _autorizacaoAppService.ValidarTokenEmail(codigoConvite, numeroCelular, email, codigoToken);

            return Response();
        }

        [HttpPost("convites/{codigoConvite}/selfie/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ValidarSelfie(string codigoConvite, [FromHeader] string numeroCelular, [FromBody] ValidarSelfieViewModel selfie)
        {
            await _autorizacaoAppService.ValidarSelfie(codigoConvite, numeroCelular, selfie.FotoBase64);

            return Response();
        }
    }
}