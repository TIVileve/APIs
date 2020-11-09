using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VilevePay.Application.Interfaces;
using VilevePay.Application.ViewModels.v1.Autorizacao;
using VilevePay.Domain.Commands.Autorizacao;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;

namespace VilevePay.Application.Services
{
    public class AutorizacaoAppService : IAutorizacaoAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;

        public AutorizacaoAppService(
            IMapper mapper,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
        {
            _mapper = mapper;
            _bus = bus;
            _notifications = (DomainNotificationHandler)notifications;
        }

        public async Task<object> Login(string email, string senha)
        {
            var loginCommand = new LoginCommand(email, senha);
            var loginResponse = await _bus.SendCommand(loginCommand);

            return _notifications.HasNotifications()
                ? loginResponse
                : new TokenViewModel
                {
                    AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c",
                    TokenType = "bearer",
                    ExpiresIn = DateTime.UtcNow.AddHours(1)
                };
        }

        public void CadastrarSenha(string codigoConvite, string email, string senha, string confirmarSenha)
        {
            var cadastrarSenhaCommand = new CadastrarSenhaCommand(codigoConvite, email, senha, confirmarSenha);
            _bus.SendCommand(cadastrarSenhaCommand);
        }

        public async Task ValidarCodigoConvite(string codigoConvite)
        {
            var validarCodigoConviteCommand = new ValidarCodigoConviteCommand(codigoConvite);
            await _bus.SendCommand(validarCodigoConviteCommand);
        }

        public void ValidarTokenSms(string codigoConvite, string numeroCelular, string codigoToken)
        {
            var validarTokenSmsCommand = new ValidarTokenSmsCommand(codigoConvite, numeroCelular, codigoToken);
            _bus.SendCommand(validarTokenSmsCommand);
        }

        public async Task ValidarTokenEmail(string codigoConvite, string email, string codigoToken)
        {
            var validarTokenEmailCommand = new ValidarTokenEmailCommand(codigoConvite, email, codigoToken);
            await _bus.SendCommand(validarTokenEmailCommand);
        }

        public async Task EnviarTokenSms(string codigoConvite, string numeroCelular)
        {
            var enviarTokenSmsCommand = new EnviarTokenSmsCommand(codigoConvite, numeroCelular);
            await _bus.SendCommand(enviarTokenSmsCommand);
        }

        public async Task EnviarTokenEmail(string codigoConvite, string email)
        {
            var enviarTokenEmailCommand = new EnviarTokenEmailCommand(codigoConvite, email);
            await _bus.SendCommand(enviarTokenEmailCommand);
        }

        public void ValidarSelfie(string codigoConvite, string fotoBase64)
        {
            var validarSelfieCommand = new ValidarSelfieCommand(codigoConvite, fotoBase64);
            _bus.SendCommand(validarSelfieCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}