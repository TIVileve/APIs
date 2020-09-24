using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VilevePay.Application.Interfaces;
using VilevePay.Application.ViewModels.v1.Endereco;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;

namespace VilevePay.Services.Api.Controllers.v1
{
    // [Authorize]
    [ApiController]
    [Route("api/v1/enderecos")]
    [Produces("application/json")]
    public class EnderecoController : ApiController
    {
        private readonly IEnderecoAppService _enderecoAppService;
        private readonly DomainNotificationHandler _notifications;

        public EnderecoController(
            IEnderecoAppService enderecoAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
            : base(notifications, mediator)
        {
            _enderecoAppService = enderecoAppService;
            _notifications = (DomainNotificationHandler)notifications;
        }

        [HttpGet("{cep}")]
        [ProducesResponseType(typeof(EnderecoViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login(string cep)
        {
            var response = await _enderecoAppService.ObterEndereco(cep);

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }
    }
}