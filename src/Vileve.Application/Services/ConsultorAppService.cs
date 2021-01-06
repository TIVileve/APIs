using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Vileve.Application.Interfaces;
using Vileve.Application.ViewModels.v1.Consultor;
using Vileve.Domain.Commands.Consultor;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Notifications;
using Vileve.Domain.Models;

namespace Vileve.Application.Services
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

        public async Task<object> ObterEndereco(string codigoConvite, string numeroCelular)
        {
            var obterEnderecoCommand = new ObterEnderecoCommand(codigoConvite, numeroCelular);
            var obterEnderecoResponse = await _bus.SendCommand(obterEnderecoCommand);

            return _notifications.HasNotifications() ? obterEnderecoResponse : _mapper.Map<IEnumerable<EnderecoViewModel>>((IEnumerable<Endereco>)obterEnderecoResponse);
        }

        public async Task<object> ObterEnderecoPorId(string codigoConvite, string numeroCelular, Guid enderecoId)
        {
            var obterEnderecoPorIdCommand = new ObterEnderecoPorIdCommand(codigoConvite, numeroCelular, enderecoId);
            var obterEnderecoPorIdResponse = await _bus.SendCommand(obterEnderecoPorIdCommand);

            return _notifications.HasNotifications() ? obterEnderecoPorIdResponse : _mapper.Map<EnderecoPorIdViewModel>((Endereco)obterEnderecoPorIdResponse);
        }

        public void CadastrarEndereco(string codigoConvite, string numeroCelular, int tipoEndereco, string cep, string logradouro,
            int numero, string complemento, string bairro, string cidade, string estado,
            bool principal, string comprovanteBase64)
        {
            var cadastrarEnderecoCommand = new CadastrarEnderecoCommand(codigoConvite, numeroCelular, tipoEndereco, cep, logradouro,
                numero, complemento, bairro, cidade, estado,
                principal, comprovanteBase64);
            _bus.SendCommand(cadastrarEnderecoCommand);
        }

        public void DeletarEndereco(string codigoConvite, string numeroCelular, Guid enderecoId)
        {
            var deletarEnderecoCommand = new DeletarEnderecoCommand(codigoConvite, numeroCelular, enderecoId);
            _bus.SendCommand(deletarEnderecoCommand);
        }

        public void CadastrarPessoaJuridica(string codigoConvite, string numeroCelular, string cnpj, string razaoSocial, string nomeFantasia,
            string inscricaoMunicipal, string inscricaoEstadual, string codigoBanco, string agencia, string contaSemDigito,
            string digito, int tipoConta, string contratoSocialBase64, string ultimaAlteracaoBase64)
        {
            var cadastrarPessoaJuridicaCommand = new CadastrarPessoaJuridicaCommand(codigoConvite, numeroCelular, cnpj, razaoSocial, nomeFantasia,
                inscricaoMunicipal, inscricaoEstadual, codigoBanco, agencia, contaSemDigito,
                digito, tipoConta, contratoSocialBase64, ultimaAlteracaoBase64);
            _bus.SendCommand(cadastrarPessoaJuridicaCommand);
        }

        public void CadastrarRepresentante(string codigoConvite, string numeroCelular, string cpf, string nomeCompleto, int sexo,
            int estadoCivil, string nacionalidade, IEnumerable<object> emails, IEnumerable<object> telefones, string documentoFrenteBase64,
            string documentoVersoBase64)
        {
            var cadastrarRepresentanteCommand = new CadastrarRepresentanteCommand(codigoConvite, numeroCelular, cpf, nomeCompleto, sexo,
                estadoCivil, nacionalidade, emails, telefones, documentoFrenteBase64,
                documentoVersoBase64);
            _bus.SendCommand(cadastrarRepresentanteCommand);
        }

        public async Task<object> ObterStatusOnboarding(string codigoConvite, string numeroCelular)
        {
            var obterStatusOnboardingCommand = new ObterStatusOnboardingCommand(codigoConvite, numeroCelular);
            var obterStatusOnboardingResponse = await _bus.SendCommand(obterStatusOnboardingCommand);

            return _notifications.HasNotifications() ? obterStatusOnboardingResponse : _mapper.Map<StatusOnboardingViewModel>((Onboarding)obterStatusOnboardingResponse);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}