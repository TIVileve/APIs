using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vileve.Application.Interfaces;
using Vileve.Application.ViewModels.v1.Consultor;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Notifications;

namespace Vileve.Services.Api.Controllers.v1
{
    // [Authorize]
    [ApiController]
    [Route("api/v1/consultores")]
    [Produces("application/json")]
    public class ConsultorController : ApiController
    {
        private readonly IConsultorAppService _consultorAppService;

        public ConsultorController(
            IConsultorAppService consultorAppService,
            ILogger<ConsultorController> logger,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
            : base(logger, notifications, mediator)
        {
            _consultorAppService = consultorAppService;
        }

        [HttpGet("convites/{codigoConvite}/enderecos")]
        [ProducesResponseType(typeof(IEnumerable<EnderecoViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterEndereco(string codigoConvite, [FromHeader] string numeroCelular)
        {
            var response = await _consultorAppService.ObterEndereco(codigoConvite, numeroCelular);

            return Response(response);
        }

        [HttpGet("convites/{codigoConvite}/enderecos/{enderecoId}")]
        [ProducesResponseType(typeof(EnderecoPorIdViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterEnderecoPorId(string codigoConvite, [FromHeader] string numeroCelular, Guid enderecoId)
        {
            var response = await _consultorAppService.ObterEnderecoPorId(codigoConvite, numeroCelular, enderecoId);

            return Response(response);
        }

        [HttpPost("convites/{codigoConvite}/enderecos")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarEndereco(string codigoConvite, [FromHeader] string numeroCelular, [FromBody] CadastrarEnderecoViewModel endereco)
        {
            _consultorAppService.CadastrarEndereco(codigoConvite, numeroCelular, endereco.TipoEndereco, endereco.Cep, endereco.Logradouro,
                endereco.Numero, endereco.Complemento, endereco.Bairro, endereco.Cidade, endereco.Estado,
                endereco.Principal, endereco.ComprovanteBase64);

            return Response();
        }

        [HttpDelete("convites/{codigoConvite}/enderecos/{enderecoId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeletarEndereco(string codigoConvite, [FromHeader] string numeroCelular, Guid enderecoId)
        {
            _consultorAppService.DeletarEndereco(codigoConvite, numeroCelular, enderecoId);

            return Response();
        }

        [HttpPost("convites/{codigoConvite}/pessoas-juridicas")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarPessoaJuridica(string codigoConvite, [FromHeader] string numeroCelular, [FromBody] CadastrarPessoaJuridicaViewModel pessoaJuridica)
        {
            _consultorAppService.CadastrarPessoaJuridica(codigoConvite, numeroCelular, pessoaJuridica.Cnpj, pessoaJuridica.RazaoSocial, pessoaJuridica.NomeFantasia,
                pessoaJuridica.InscricaoMunicipal, pessoaJuridica.InscricaoEstadual, pessoaJuridica.DadosBancarios.CodigoBanco, pessoaJuridica.DadosBancarios.Agencia, pessoaJuridica.DadosBancarios.ContaSemDigito,
                pessoaJuridica.DadosBancarios.Digito, pessoaJuridica.DadosBancarios.TipoConta, pessoaJuridica.ContratoSocialBase64, pessoaJuridica.UltimaAlteracaoBase64);

            return Response();
        }

        [HttpPost("convites/{codigoConvite}/representantes")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarRepresentante(string codigoConvite, [FromHeader] string numeroCelular, [FromBody] CadastrarRepresentanteViewModel representante)
        {
            _consultorAppService.CadastrarRepresentante(codigoConvite, numeroCelular, representante.Cpf, representante.NomeCompleto, representante.Sexo,
                representante.EstadoCivil, representante.Nacionalidade, representante.Emails, representante.Telefones, representante.Documento.FrenteBase64,
                representante.Documento.VersoBase64);

            return Response();
        }

        [HttpGet("convites/{codigoConvite}/onboarding/status")]
        [ProducesResponseType(typeof(StatusOnboardingViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterStatusOnboarding(string codigoConvite, [FromHeader] string numeroCelular)
        {
            var response = await _consultorAppService.ObterStatusOnboarding(codigoConvite, numeroCelular);

            return Response(response);
        }
    }
}