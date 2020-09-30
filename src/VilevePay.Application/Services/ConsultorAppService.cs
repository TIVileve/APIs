using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VilevePay.Application.Interfaces;
using VilevePay.Domain.Commands.Consultor;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;

namespace VilevePay.Application.Services
{
    public class ConsultorAppService : IConsultorAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;

        public ConsultorAppService(
            IMapper mapper,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
        {
            _mapper = mapper;
            _bus = bus;
            _notifications = (DomainNotificationHandler)notifications;
        }

        public async Task<object> ObterStatusOnboarding(string codigoConvite)
        {
            var obterStatusOnboardingCommand = new ObterStatusOnboardingCommand(codigoConvite);
            var obterStatusOnboardingResponse = await _bus.SendCommand(obterStatusOnboardingCommand);

            return _notifications.HasNotifications()
                ? obterStatusOnboardingResponse
                : new
                {
                };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}