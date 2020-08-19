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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}