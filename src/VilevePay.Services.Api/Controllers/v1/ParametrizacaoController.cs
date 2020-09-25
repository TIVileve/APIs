using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VilevePay.Application.Interfaces;
using VilevePay.Application.ViewModels.v1.Parametrizacao;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;

namespace VilevePay.Services.Api.Controllers.v1
{
    // [Authorize]
    [ApiController]
    [Route("api/v1/parametrizacoes")]
    [Produces("application/json")]
    public class ParametrizacaoController : ApiController
    {
        private readonly IParametrizacaoAppService _parametrizacaoAppService;
        private readonly DomainNotificationHandler _notifications;

        public ParametrizacaoController(
            IParametrizacaoAppService parametrizacaoAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
            : base(notifications, mediator)
        {
            _parametrizacaoAppService = parametrizacaoAppService;
            _notifications = (DomainNotificationHandler)notifications;
        }

        [HttpGet("estados-civis")]
        [ProducesResponseType(typeof(IEnumerable<EstadoCivilViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterEstadoCivil()
        {
            var response = await _parametrizacaoAppService.ObterEstadoCivil();

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("nacionalidades")]
        [ProducesResponseType(typeof(IEnumerable<NacionalidadeViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterNacionalidade()
        {
            var response = await _parametrizacaoAppService.ObterNacionalidade();

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("perfil-usuarios")]
        [ProducesResponseType(typeof(IEnumerable<PerfilUsuarioViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterPerfilUsuario()
        {
            var response = await _parametrizacaoAppService.ObterPerfilUsuario();

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("tipos-telefone")]
        [ProducesResponseType(typeof(IEnumerable<TipoTelefoneViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterTipoTelefone()
        {
            var response = await _parametrizacaoAppService.ObterTipoTelefone();

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("tipos-email")]
        [ProducesResponseType(typeof(IEnumerable<TipoEmailViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterTipoEmail()
        {
            var response = await _parametrizacaoAppService.ObterTipoEmail();

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("tipos-endereco")]
        [ProducesResponseType(typeof(IEnumerable<TipoEnderecoViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterTipoEndereco()
        {
            var response = await _parametrizacaoAppService.ObterTipoEndereco();

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("bancos")]
        [ProducesResponseType(typeof(IEnumerable<BancoViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterBanco()
        {
            var response = await _parametrizacaoAppService.ObterBanco();

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("operacoes-bancarias")]
        [ProducesResponseType(typeof(IEnumerable<OperacaoBancariaViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterOperacaoBancaria()
        {
            var response = await _parametrizacaoAppService.ObterOperacaoBancaria();

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }
    }
}