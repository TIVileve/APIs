using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Vileve.Application.Interfaces;
using Vileve.Application.ViewModels.v1.Autorizacao;
using Vileve.Domain.Commands.Autorizacao;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Notifications;
using Vileve.Domain.Responses;

namespace Vileve.Application.Services
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

            return _notifications.HasNotifications() ? loginResponse : _mapper.Map<TokenViewModel>((Token)loginResponse);
        }

        public void CadastrarSenha(string codigoConvite, string numeroCelular, string email, string senha,
            string confirmarSenha)
        {
            var cadastrarSenhaCommand = new CadastrarSenhaCommand(codigoConvite, numeroCelular, email, senha,
                confirmarSenha);
            _bus.SendCommand(cadastrarSenhaCommand);
        }

        public async Task ValidarCodigoConvite(string codigoConvite)
        {
            var validarCodigoConviteCommand = new ValidarCodigoConviteCommand(codigoConvite);
            await _bus.SendCommand(validarCodigoConviteCommand);
        }

        public async Task ValidarTokenSms(string codigoConvite, string numeroCelular, string codigoToken)
        {
            var validarTokenSmsCommand = new ValidarTokenSmsCommand(codigoConvite, numeroCelular, codigoToken);
            await _bus.SendCommand(validarTokenSmsCommand);
        }

        public async Task ValidarTokenEmail(string codigoConvite, string numeroCelular, string email, string codigoToken)
        {
            var validarTokenEmailCommand = new ValidarTokenEmailCommand(codigoConvite, numeroCelular, email, codigoToken);
            await _bus.SendCommand(validarTokenEmailCommand);
        }

        public async Task EnviarTokenSms(string numeroCelular)
        {
            var enviarTokenSmsCommand = new EnviarTokenSmsCommand(numeroCelular);
            await _bus.SendCommand(enviarTokenSmsCommand);
        }

        public async Task EnviarTokenEmail(string email)
        {
            var enviarTokenEmailCommand = new EnviarTokenEmailCommand(email);
            await _bus.SendCommand(enviarTokenEmailCommand);
        }

        public async Task ValidarSelfie(string codigoConvite, string numeroCelular, string fotoBase64)
        {
            var validarSelfieCommand = new ValidarSelfieCommand(codigoConvite, numeroCelular, fotoBase64);
            await _bus.SendCommand(validarSelfieCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}