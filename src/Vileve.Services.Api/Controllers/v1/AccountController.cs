using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Notifications;
using Vileve.Domain.Interfaces;
using Vileve.Infra.CrossCutting.Identity.Models;
using Vileve.Infra.CrossCutting.Io.Http;
using Vileve.Services.Api.Configurations;

namespace Vileve.Services.Api.Controllers.v1
{
    [Authorize]
    [ApiController]
    [Route("api/v1/account")]
    [Produces("application/json")]
    public class AccountController : ApiController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly IUser _user;
        private readonly ServiceManager _serviceManager;
        private readonly IHttpAppService _httpAppService;

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<AppSettings> appSettings,
            IUser user,
            IOptions<ServiceManager> serviceManager,
            IHttpAppService httpAppService,
            ILogger<AccountController> logger,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
            : base(logger, notifications, mediator)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _user = user;
            _serviceManager = serviceManager.Value;
            _httpAppService = httpAppService;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegistration userRegistration)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(userRegistration);
            }

            var user = new IdentityUser
            {
                UserName = userRegistration.Email,
                Email = userRegistration.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, userRegistration.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    NotifyError(error.Code, error.Description);
                }

                return Response(userRegistration);
            }

            await _signInManager.SignInAsync(user, false);
            var token = await GenerateJwt(userRegistration.Email);

            return Ok(new
            {
                access_token = token,
                token_type = "bearer",
                expires_in = DateTime.UtcNow.AddHours(_appSettings.Expiration),
                userName = user.Email
            });
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(userLogin);
            }

            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);

            if (result.Succeeded)
            {
                var token = await GenerateJwt(userLogin.Email);

                return Ok(new
                {
                    access_token = token,
                    token_type = "bearer",
                    expires_in = DateTime.UtcNow.AddHours(_appSettings.Expiration),
                    userName = userLogin.Email
                });
            }

            NotifyError("Login", "E-MAIL ou SENHA inválidos.");
            return Response(userLogin);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("external-login")]
        public async Task<IActionResult> ExternalLogin(UserLogin userLogin)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(userLogin);
            }

            var client = _httpAppService.CreateClient(_serviceManager.UrlVileve);

            var result = new ExternalLoginToken();

            try
            {
                result = await _httpAppService.OnPost<ExternalLoginToken, object>(client, Guid.NewGuid(), "v1/auth/login", new
                {
                    usuario = userLogin.Email,
                    senha = userLogin.Password
                });
            }
            catch (Exception)
            {
                // ignored
            }

            if (result.Success)
            {
                var token = GenerateJwt(userLogin.Email, result.Token);

                return Ok(new
                {
                    access_token = token,
                    token_type = "bearer",
                    expires_in = DateTime.UtcNow.AddHours(1),
                    userName = userLogin.Email
                });
            }

            NotifyError("Login", "E-MAIL ou SENHA inválidos.");
            return Response(userLogin);
        }

        [HttpGet]
        [Route("user-info")]
        public IActionResult GetUserInfo()
        {
            if (_user.IsAuthenticated())
                return Ok(new
                {
                    Nome = _user.Name,
                    Sobrenome = "",
                    Email = _user.Name
                });

            return BadRequest();
        }

        private async Task<string> GenerateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidAt,
                Expires = DateTime.UtcNow.AddHours(_appSettings.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        private string GenerateJwt(string email, string token)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim("Token", token)
            };

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