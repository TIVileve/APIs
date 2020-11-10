using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VilevePay.Application.Interfaces;
using VilevePay.Application.ViewModels.v1.Consultor;
using VilevePay.Domain.Commands.Consultor;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;
using VilevePay.Domain.Models;

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

        public async Task<object> ObterEndereco(string codigoConvite)
        {
            var obterEnderecoCommand = new ObterEnderecoCommand(codigoConvite);
            var obterEnderecoResponse = await _bus.SendCommand(obterEnderecoCommand);

            return _notifications.HasNotifications() ? obterEnderecoResponse : _mapper.Map<IEnumerable<EnderecoViewModel>>((IEnumerable<Endereco>)obterEnderecoResponse);
        }

        public async Task<object> ObterEnderecoPorId(string codigoConvite, Guid enderecoId)
        {
            var obterEnderecoPorIdCommand = new ObterEnderecoPorIdCommand(codigoConvite, enderecoId);
            var obterEnderecoPorIdResponse = await _bus.SendCommand(obterEnderecoPorIdCommand);

            return _notifications.HasNotifications() ? obterEnderecoPorIdResponse : _mapper.Map<EnderecoViewModel>((Endereco)obterEnderecoPorIdResponse);
        }

        public void CadastrarEndereco(string codigoConvite, int tipoEndereco, string cep, string logradouro, int numero,
            string complemento, string bairro, string cidade, string estado, bool principal,
            string comprovanteBase64)
        {
            var cadastrarEnderecoCommand = new CadastrarEnderecoCommand(codigoConvite, tipoEndereco, cep, logradouro, numero,
                complemento, bairro, cidade, estado, principal,
                comprovanteBase64);
            _bus.SendCommand(cadastrarEnderecoCommand);
        }

        public void DeletarEndereco(string codigoConvite, Guid enderecoId)
        {
            var deletarEnderecoCommand = new DeletarEnderecoCommand(codigoConvite, enderecoId);
            _bus.SendCommand(deletarEnderecoCommand);
        }

        public void CadastrarPessoaJuridica(string codigoConvite, string cnpj, string razaoSocial, string nomeFantasia, string inscricaoMunicipal,
            string inscricaoEstadual, string codigoBanco, string agencia, string contaSemDigito, string digito,
            int tipoConta, string contratoSocialBase64, string ultimaAlteracaoBase64)
        {
            var cadastrarPessoaJuridicaCommand = new CadastrarPessoaJuridicaCommand(codigoConvite, cnpj, razaoSocial, nomeFantasia, inscricaoMunicipal,
                inscricaoEstadual, codigoBanco, agencia, contaSemDigito, digito,
                tipoConta, contratoSocialBase64, ultimaAlteracaoBase64);
            _bus.SendCommand(cadastrarPessoaJuridicaCommand);
        }

        public void CadastrarRepresentante(string codigoConvite)
        {
            var cadastrarRepresentanteCommand = new CadastrarRepresentanteCommand(codigoConvite);
            _bus.SendCommand(cadastrarRepresentanteCommand);
        }

        public async Task<object> ObterStatusOnboarding(string email)
        {
            var obterStatusOnboardingCommand = new ObterStatusOnboardingCommand(email);
            var obterStatusOnboardingResponse = await _bus.SendCommand(obterStatusOnboardingCommand);

            return _notifications.HasNotifications()
                ? obterStatusOnboardingResponse
                : new StatusOnboardingViewModel
                {
                    CodigoConvite = "123456",
                    Status = StatusViewModel.ComprovanteEndereco
                };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}