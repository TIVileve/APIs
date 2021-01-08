using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vileve.Application.Interfaces;
using Vileve.Application.ViewModels.v1.Endereco;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Notifications;

namespace Vileve.Services.Api.Controllers.v1
{
    // [Authorize]
    [ApiController]
    [Route("api/v1/enderecos")]
    [Produces("application/json")]
    public class EnderecoController : ApiController
    {
        private readonly IEnderecoAppService _enderecoAppService;

        public EnderecoController(
            IEnderecoAppService enderecoAppService,
            ILogger<EnderecoController> logger,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
            : base(logger, notifications, mediator)
        {
            _enderecoAppService = enderecoAppService;
        }

        [HttpGet("{cep}")]
        [ProducesResponseType(typeof(EnderecoViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterEndereco(string cep)
        {
            var response = await _enderecoAppService.ObterEndereco(cep);

            return Response(response);
        }
    }
}