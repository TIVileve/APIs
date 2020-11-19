using System;
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

        [HttpGet("convites/{codigoConvite}/enderecos")]
        [ProducesResponseType(typeof(IEnumerable<EnderecoViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterEndereco(string codigoConvite, [FromHeader] string numeroCelular)
        {
            var response = await _consultorAppService.ObterEndereco(codigoConvite, numeroCelular);

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("convites/{codigoConvite}/enderecos/{enderecoId}")]
        [ProducesResponseType(typeof(EnderecoViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterEnderecoPorId(string codigoConvite, [FromHeader] string numeroCelular, Guid enderecoId)
        {
            var response = await _consultorAppService.ObterEnderecoPorId(codigoConvite, numeroCelular, enderecoId);

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("convites/{codigoConvite}/enderecos")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarEndereco(string codigoConvite, [FromHeader] string numeroCelular, [FromBody] CadastrarEnderecoViewModel endereco)
        {
            _consultorAppService.CadastrarEndereco(codigoConvite, numeroCelular, endereco.TipoEndereco, endereco.Cep, endereco.Logradouro,
                endereco.Numero, endereco.Complemento, endereco.Bairro, endereco.Cidade, endereco.Estado,
                endereco.Principal, endereco.ComprovanteBase64);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpDelete("convites/{codigoConvite}/enderecos/{enderecoId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeletarEndereco(string codigoConvite, [FromHeader] string numeroCelular, Guid enderecoId)
        {
            _consultorAppService.DeletarEndereco(codigoConvite, numeroCelular, enderecoId);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("convites/{codigoConvite}/pessoas-juridicas")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarPessoaJuridica(string codigoConvite, [FromHeader] string numeroCelular, [FromBody] CadastrarPessoaJuridicaViewModel pessoaJuridica)
        {
            _consultorAppService.CadastrarPessoaJuridica(codigoConvite, numeroCelular, pessoaJuridica.Cnpj, pessoaJuridica.RazaoSocial, pessoaJuridica.NomeFantasia,
                pessoaJuridica.InscricaoMunicipal, pessoaJuridica.InscricaoEstadual, pessoaJuridica.DadosBancarios.CodigoBanco, pessoaJuridica.DadosBancarios.Agencia, pessoaJuridica.DadosBancarios.ContaSemDigito,
                pessoaJuridica.DadosBancarios.Digito, pessoaJuridica.DadosBancarios.TipoConta, pessoaJuridica.ContratoSocialBase64, pessoaJuridica.UltimaAlteracaoBase64);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpPost("convites/{codigoConvite}/representantes")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarRepresentante(string codigoConvite, [FromBody] CadastrarRepresentanteViewModel representante)
        {
            _consultorAppService.CadastrarRepresentante(codigoConvite, representante.Cpf, representante.NomeCompleto, representante.Sexo, representante.EstadoCivil,
                representante.Nacionalidade, representante.Emails, representante.Telefones, representante.Documento.FrenteBase64, representante.Documento.VersoBase64);

            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }

        [HttpGet("onboarding/status")]
        [ProducesResponseType(typeof(StatusOnboardingViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterStatusOnboarding([FromHeader] string email)
        {
            var response = await _consultorAppService.ObterStatusOnboarding(email);

            if (IsValidOperation())
            {
                return Ok(response);
            }

            return BadRequest(_notifications.GetNotifications().Select(n => n.Value));
        }
    }
}