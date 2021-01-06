using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Vileve.Application.Interfaces;
using Vileve.Application.ViewModels.v1.Endereco;
using Vileve.Domain.Commands.Endereco;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Notifications;
using Vileve.Domain.Responses;

namespace Vileve.Application.Services
{
    public class EnderecoAppService : IEnderecoAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;

        public EnderecoAppService(
            IMapper mapper,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
        {
            _mapper = mapper;
            _bus = bus;
            _notifications = (DomainNotificationHandler)notifications;
        }

        public async Task<object> ObterEndereco(string cep)
        {
            var obterEnderecoCommand = new ObterEnderecoCommand(cep);
            var obterEnderecoResponse = await _bus.SendCommand(obterEnderecoCommand);

            return _notifications.HasNotifications() ? obterEnderecoResponse : _mapper.Map<EnderecoViewModel>((EnderecoCep)obterEnderecoResponse);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}