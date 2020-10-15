using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VilevePay.Application.Interfaces;
using VilevePay.Application.ViewModels.v1.Consultor;
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

        public void CadastrarDadosBancarios(string codigoConvite)
        {
            var cadastrarDadosBancariosCommand = new CadastrarDadosBancariosCommand(codigoConvite);
            _bus.SendCommand(cadastrarDadosBancariosCommand);
        }

        public void CadastrarEmail(string codigoConvite)
        {
            var cadastrarEmailCommand = new CadastrarEmailCommand(codigoConvite);
            _bus.SendCommand(cadastrarEmailCommand);
        }

        public void CadastrarEndereco(string codigoConvite)
        {
            var cadastrarEnderecoCommand = new CadastrarEnderecoCommand(codigoConvite);
            _bus.SendCommand(cadastrarEnderecoCommand);
        }

        public void CadastrarPessoaJuridica(string codigoConvite)
        {
            var cadastrarPessoaJuridicaCommand = new CadastrarPessoaJuridicaCommand(codigoConvite);
            _bus.SendCommand(cadastrarPessoaJuridicaCommand);
        }

        public void CadastrarTelefone(string codigoConvite)
        {
            var cadastrarTelefoneCommand = new CadastrarTelefoneCommand(codigoConvite);
            _bus.SendCommand(cadastrarTelefoneCommand);
        }

        public async Task<object> ObterStatusOnboarding(string codigoConvite)
        {
            var obterStatusOnboardingCommand = new ObterStatusOnboardingCommand(codigoConvite);
            var obterStatusOnboardingResponse = await _bus.SendCommand(obterStatusOnboardingCommand);

            return _notifications.HasNotifications()
                ? obterStatusOnboardingResponse
                : StatusOnboardingViewModel.ComprovanteEndereco;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}