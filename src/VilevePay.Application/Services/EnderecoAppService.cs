using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VilevePay.Application.Interfaces;
using VilevePay.Application.ViewModels.v1.Endereco;
using VilevePay.Domain.Commands.Endereco;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;

namespace VilevePay.Application.Services
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

            return _notifications.HasNotifications()
                ? obterEnderecoResponse
                : new EnderecoViewModel
                {
                    CodigoCidade = 1,
                    CodigoUf = 1,
                    IbgeMunicipio = 1,
                    Cidade = "Nova Lima",
                    Logradouro = "Rua da Mata",
                    TipoLogradouro = "Rua",
                    Bairro = "Vila da Serra",
                    Uf = "MG",
                    Resultado = 1
                };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}