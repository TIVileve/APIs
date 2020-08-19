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

        public void ValidarCodigoToken(string codigoConvite, string numeroCelular, string codigoToken)
        {
            var validarCodigoTokenCommand = new ValidarCodigoTokenCommand(codigoConvite, numeroCelular, codigoToken);
            _bus.SendCommand(validarCodigoTokenCommand);
        }

        public void ValidarEmail(string codigoConvite, string email)
        {
            var validarEmailCommand = new ValidarEmailCommand(codigoConvite, email);
            _bus.SendCommand(validarEmailCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}