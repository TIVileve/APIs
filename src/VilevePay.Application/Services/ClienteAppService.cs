using System;
using AutoMapper;
using MediatR;
using VilevePay.Application.Interfaces;
using VilevePay.Domain.Commands.Cliente;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;

namespace VilevePay.Application.Services
{
    public class ClienteAppService : IClienteAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;

        public ClienteAppService(
            IMapper mapper,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
        {
            _mapper = mapper;
            _bus = bus;
            _notifications = (DomainNotificationHandler)notifications;
        }

        public void CadastrarCliente()
        {
            var cadastrarClienteCommand = new CadastrarClienteCommand();
            _bus.SendCommand(cadastrarClienteCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}