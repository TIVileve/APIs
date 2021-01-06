using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vileve.Application.Interfaces;
using Vileve.Application.ViewModels.v1.Parametrizacao;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Notifications;

namespace Vileve.Services.Api.Controllers.v1
{
    // [Authorize]
    [ApiController]
    [Route("api/v1/parametrizacoes")]
    [Produces("application/json")]
    public class ParametrizacaoController : ApiController
    {
        private readonly IParametrizacaoAppService _parametrizacaoAppService;
        private readonly ILogger<ParametrizacaoController> _logger;
        private readonly DomainNotificationHandler _notifications;

        public ParametrizacaoController(
            IParametrizacaoAppService parametrizacaoAppService,
            ILogger<ParametrizacaoController> logger,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
            : base(notifications, mediator)
        {
            _parametrizacaoAppService = parametrizacaoAppService;
            _logger = logger;
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

            if (_notifications.HasNotifications())
            {
                _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
                {
                    parameters = new { },
                    errors = _notifications.GetNotifications().Select(n => n)
                }));
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

            if (_notifications.HasNotifications())
            {
                _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
                {
                    parameters = new { },
                    errors = _notifications.GetNotifications().Select(n => n)
                }));
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

            if (_notifications.HasNotifications())
            {
                _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
                {
                    parameters = new { },
                    errors = _notifications.GetNotifications().Select(n => n)
                }));
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

            if (_notifications.HasNotifications())
            {
                _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
                {
                    parameters = new { },
                    errors = _notifications.GetNotifications().Select(n => n)
                }));
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

            if (_notifications.HasNotifications())
            {
                _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
                {
                    parameters = new { },
                    errors = _notifications.GetNotifications().Select(n => n)
                }));
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

            if (_notifications.HasNotifications())
            {
                _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
                {
                    parameters = new { },
                    errors = _notifications.GetNotifications().Select(n => n)
                }));
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

            if (_notifications.HasNotifications())
            {
                _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
                {
                    parameters = new { },
                    errors = _notifications.GetNotifications().Select(n => n)
                }));
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

            if (_notifications.HasNotifications())
            {
                _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
                {
                    parameters = new { },
                    errors = _notifications.GetNotifications().Select(n => n)
                }));
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("sexos")]
        [ProducesResponseType(typeof(IEnumerable<SexoViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterSexo()
        {
            var response = await _parametrizacaoAppService.ObterSexo();

            if (IsValidOperation())
            {
                return Ok(response);
            }

            if (_notifications.HasNotifications())
            {
                _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
                {
                    parameters = new { },
                    errors = _notifications.GetNotifications().Select(n => n)
                }));
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("tipos-parentesco")]
        [ProducesResponseType(typeof(IEnumerable<ParentescoViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterTipoParentesco()
        {
            var response = await _parametrizacaoAppService.ObterTipoParentesco();

            if (IsValidOperation())
            {
                return Ok(response);
            }

            if (_notifications.HasNotifications())
            {
                _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
                {
                    parameters = new { },
                    errors = _notifications.GetNotifications().Select(n => n)
                }));
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("tipos-pagamento")]
        [ProducesResponseType(typeof(IEnumerable<PagamentoViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterTipoPagamento()
        {
            var response = await _parametrizacaoAppService.ObterTipoPagamento();

            if (IsValidOperation())
            {
                return Ok(response);
            }

            if (_notifications.HasNotifications())
            {
                _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
                {
                    parameters = new { },
                    errors = _notifications.GetNotifications().Select(n => n)
                }));
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("tipos-convenio")]
        [ProducesResponseType(typeof(IEnumerable<ConvenioViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterTipoConvenio()
        {
            var response = await _parametrizacaoAppService.ObterTipoConvenio();

            if (IsValidOperation())
            {
                return Ok(response);
            }

            if (_notifications.HasNotifications())
            {
                _logger.Log(LogLevel.Warning, JsonSerializer.Serialize(new
                {
                    parameters = new { },
                    errors = _notifications.GetNotifications().Select(n => n)
                }));
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }
    }
}