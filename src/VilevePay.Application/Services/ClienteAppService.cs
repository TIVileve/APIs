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

        public async Task<object> CadastrarCliente()
        {
            var cadastrarClienteCommand = new CadastrarClienteCommand();
            var cadastrarClienteResponse = await _bus.SendCommand(cadastrarClienteCommand);

            return _notifications.HasNotifications()
                ? cadastrarClienteResponse
                : new ClienteViewModel
                {
                    Id = Guid.NewGuid()
                };
        }

        public async Task<object> ObterProduto()
        {
            var obterProdutoCommand = new ObterProdutoCommand();
            var obterProdutoResponse = await _bus.SendCommand(obterProdutoCommand);

            return _notifications.HasNotifications() ? obterProdutoResponse : _mapper.Map<ProdutoViewModel>((SeguroProduto)obterProdutoResponse);
        }

        public void CadastrarProduto(Guid clienteId)
        {
            var cadastrarProdutoCommand = new CadastrarProdutoCommand(clienteId);
            _bus.SendCommand(cadastrarProdutoCommand);
        }

        public void CadastrarEndereco(Guid clienteId, string cep, string logradouro, int numero, string complemento,
            string bairro, string cidade, string estado)
        {
            var cadastrarEnderecoCommand = new CadastrarEnderecoCommand(clienteId, cep, logradouro, numero, complemento,
                bairro, cidade, estado);
            _bus.SendCommand(cadastrarEnderecoCommand);
        }

        public void CadastrarDependente(Guid clienteId)
        {
            var cadastrarDependenteCommand = new CadastrarDependenteCommand(clienteId);
            _bus.SendCommand(cadastrarDependenteCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}