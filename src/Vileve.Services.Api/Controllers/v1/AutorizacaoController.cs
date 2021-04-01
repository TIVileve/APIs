using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Vileve.Application.Interfaces;
using Vileve.Application.ViewModels.v1.Autorizacao;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Notifications;
using Vileve.Services.Api.Configurations;

namespace Vileve.Services.Api.Controllers.v1
{
    // [Authorize]
    [ApiController]
    [Route("api/v1/autorizacoes")]
    [Produces("application/json")]
    public class AutorizacaoController : ApiController
    {
        private readonly IAutorizacaoAppService _autorizacaoAppService;
        private readonly AppSettings _appSettings;

        public AutorizacaoController(
            IAutorizacaoAppService autorizacaoAppService,
            IOptions<AppSettings> appSettings,
            ILogger<AutorizacaoController> logger,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
            : base(logger, notifications, mediator)
        {
            _autorizacaoAppService = autorizacaoAppService;
            _appSettings = appSettings.Value;
        }

        [HttpGet("login")]
        [ProducesResponseType(typeof(TokenViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromHeader] string email, [FromHeader] string senha)
        {
            var response = await _autorizacaoAppService.Login(email, senha);

            if (IsValidOperation() && response is TokenViewModel token)
            {
                token.AccessToken = GenerateJwt(email, token.AccessToken, token.ConsultorId);
                token.TokenType = "bearer";
                token.ExpiresIn = DateTime.UtcNow.AddHours(1);
            }

            return Response(response);
        }

        [HttpGet("sms/token/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ValidarTokenSms([FromHeader] string numeroCelular, [FromHeader] string codigoToken)
        {
            await _autorizacaoAppService.ValidarTokenSms("******", numeroCelular, codigoToken);

            return Response();
        }

        [HttpPost("sms/token")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EnviarTokenSms([FromHeader] string numeroCelular)
        {
            await _autorizacaoAppService.EnviarTokenSms(numeroCelular);

            return Response();
        }

        [HttpPost("e-mails/token")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EnviarTokenEmail([FromHeader] string email)
        {
            await _autorizacaoAppService.EnviarTokenEmail(email);

            return Response();
        }

        [HttpPost("convites/{codigoConvite}/senhas")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CadastrarSenha(string codigoConvite, [FromHeader] string numeroCelular, [FromHeader] string email, [FromHeader] string senha,
            [FromHeader] string confirmarSenha)
        {
            _autorizacaoAppService.CadastrarSenha(codigoConvite, numeroCelular, email, senha,
                confirmarSenha);

            return Response();
        }

        [HttpGet("convites/{codigoConvite}/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ValidarCodigoConvite(string codigoConvite)
        {
            await _autorizacaoAppService.ValidarCodigoConvite(codigoConvite);

            return Response();
        }

        [HttpGet("convites/{codigoConvite}/sms/token/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ValidarTokenSms(string codigoConvite, [FromHeader] string numeroCelular, [FromHeader] string codigoToken)
        {
            await _autorizacaoAppService.ValidarTokenSms(codigoConvite, numeroCelular, codigoToken);

            return Response();
        }

        [HttpGet("convites/{codigoConvite}/e-mails/token/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ValidarTokenEmail(string codigoConvite, [FromHeader] string numeroCelular, [FromHeader] string email, [FromHeader] string codigoToken)
        {
            await _autorizacaoAppService.ValidarTokenEmail(codigoConvite, numeroCelular, email, codigoToken);

            return Response();
        }

        [HttpPost("convites/{codigoConvite}/selfie/validar")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ValidarSelfie(string codigoConvite, [FromHeader] string numeroCelular, [FromBody] ValidarSelfieViewModel selfie)
        {
            await _autorizacaoAppService.ValidarSelfie(codigoConvite, numeroCelular, selfie.FotoBase64);

            return Response();
        }

        private string GenerateJwt(string email, string token, Guid? consultorId)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim("Token", token)
            };

            if (consultorId.HasValue)
                claims.Add(new Claim("ConsultorId", consultorId.Value.ToString()));

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidAt,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}