using System.Collections.Generic;
using System.Net;
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

        public ParametrizacaoController(
            IParametrizacaoAppService parametrizacaoAppService,
            ILogger<ParametrizacaoController> logger,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
            : base(logger, notifications, mediator)
        {
            _parametrizacaoAppService = parametrizacaoAppService;
        }

        [HttpGet("estados-civis")]
        [ProducesResponseType(typeof(IEnumerable<EstadoCivilViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterEstadoCivil()
        {
            var response = await _parametrizacaoAppService.ObterEstadoCivil();

            return Response(response);
        }

        [HttpGet("nacionalidades")]
        [ProducesResponseType(typeof(IEnumerable<NacionalidadeViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterNacionalidade()
        {
            var response = await _parametrizacaoAppService.ObterNacionalidade();

            return Response(response);
        }

        [HttpGet("perfil-usuarios")]
        [ProducesResponseType(typeof(IEnumerable<PerfilUsuarioViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterPerfilUsuario()
        {
            var response = await _parametrizacaoAppService.ObterPerfilUsuario();

            return Response(response);
        }

        [HttpGet("tipos-telefone")]
        [ProducesResponseType(typeof(IEnumerable<TipoTelefoneViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterTipoTelefone()
        {
            var response = await _parametrizacaoAppService.ObterTipoTelefone();

            return Response(response);
        }

        [HttpGet("tipos-email")]
        [ProducesResponseType(typeof(IEnumerable<TipoEmailViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterTipoEmail()
        {
            var response = await _parametrizacaoAppService.ObterTipoEmail();

            return Response(response);
        }

        [HttpGet("tipos-endereco")]
        [ProducesResponseType(typeof(IEnumerable<TipoEnderecoViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterTipoEndereco()
        {
            var response = await _parametrizacaoAppService.ObterTipoEndereco();

            return Response(response);
        }

        [HttpGet("bancos")]
        [ProducesResponseType(typeof(IEnumerable<BancoViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterBanco()
        {
            var response = await _parametrizacaoAppService.ObterBanco();

            return Response(response);
        }

        [HttpGet("operacoes-bancarias")]
        [ProducesResponseType(typeof(IEnumerable<OperacaoBancariaViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterOperacaoBancaria()
        {
            var response = await _parametrizacaoAppService.ObterOperacaoBancaria();

            return Response(response);
        }

        [HttpGet("sexos")]
        [ProducesResponseType(typeof(IEnumerable<SexoViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterSexo()
        {
            var response = await _parametrizacaoAppService.ObterSexo();

            return Response(response);
        }

        [HttpGet("tipos-parentesco")]
        [ProducesResponseType(typeof(IEnumerable<ParentescoViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterTipoParentesco()
        {
            var response = await _parametrizacaoAppService.ObterTipoParentesco();

            return Response(response);
        }

        [HttpGet("tipos-pagamento")]
        [ProducesResponseType(typeof(IEnumerable<PagamentoViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterTipoPagamento()
        {
            var response = await _parametrizacaoAppService.ObterTipoPagamento();

            return Response(response);
        }

        [HttpGet("tipos-convenio")]
        [ProducesResponseType(typeof(IEnumerable<ConvenioViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterTipoConvenio()
        {
            var response = await _parametrizacaoAppService.ObterTipoConvenio();

            return Response(response);
        }
    }
}