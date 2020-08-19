using System.Collections.Generic;
using System.Linq;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VilevePay.Application.Interfaces;
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

        [HttpGet("convites/{codigoConvite}/tokens/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult ValidarCodigoToken(string codigoConvite, [FromHeader] string codigoToken)
        {
            _autorizacaoAppService.ValidarCodigoToken(codigoConvite, codigoToken);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("convites/{codigoConvite}/e-mails/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult ValidarEmail(string codigoConvite, [FromHeader] string email)
        {
            _autorizacaoAppService.ValidarEmail(codigoConvite, email);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }
    }
}