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

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarCliente([FromBody] CadastrarClienteViewModel cliente)
        {
            _clienteAppService.CadastrarCliente();

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }
    }
}