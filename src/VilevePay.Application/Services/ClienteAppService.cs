using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VilevePay.Application.Interfaces;
using VilevePay.Application.ViewModels.v1.Cliente;
using VilevePay.Domain.Commands.Cliente;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;
using VilevePay.Domain.Responses;

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

        public async Task<object> ObterProduto()
        {
            var obterProdutoCommand = new ObterProdutoCommand();
            var obterProdutoResponse = await _bus.SendCommand(obterProdutoCommand);

            return _notifications.HasNotifications() ? obterProdutoResponse : _mapper.Map<ProdutoViewModel>((SeguroProduto)obterProdutoResponse);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}