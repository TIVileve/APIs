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