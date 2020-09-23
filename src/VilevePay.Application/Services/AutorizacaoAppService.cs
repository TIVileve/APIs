using System;
using AutoMapper;
using MediatR;
using VilevePay.Application.Interfaces;
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

        public void ValidarCodigoConvite(string codigoConvite)
        {
            var validarCodigoConviteCommand = new ValidarCodigoConviteCommand(codigoConvite);
            _bus.SendCommand(validarCodigoConviteCommand);
        }

        public void ValidarSms(string codigoConvite, string numeroCelular, string codigoToken)
        {
            var validarSmsCommand = new ValidarSmsCommand(codigoConvite, numeroCelular, codigoToken);
            _bus.SendCommand(validarSmsCommand);
        }

        public void ValidarEmail(string codigoConvite, string email, string codigoToken)
        {
            var validarEmailCommand = new ValidarEmailCommand(codigoConvite, email, codigoToken);
            _bus.SendCommand(validarEmailCommand);
        }

        public void EnviarVerificadorSms(string codigoConvite, string numeroCelular)
        {
            var enviarVerificadorSmsCommand = new EnviarVerificadorSmsCommand(codigoConvite, numeroCelular);
            _bus.SendCommand(enviarVerificadorSmsCommand);
        }

        public void EnviarVerificadorEmail(string codigoConvite, string email)
        {
            var enviarVerificadorEmailCommand = new EnviarVerificadorEmailCommand(codigoConvite, email);
            _bus.SendCommand(enviarVerificadorEmailCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}