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

        public void CadastrarArquivo(string codigoConvite)
        {
            var cadastrarArquivoCommand = new CadastrarArquivoCommand(codigoConvite);
            _bus.SendCommand(cadastrarArquivoCommand);
        }

        public void CadastrarDadosBancarios(string codigoConvite)
        {
            var cadastrarDadosBancariosCommand = new CadastrarDadosBancariosCommand(codigoConvite);
            _bus.SendCommand(cadastrarDadosBancariosCommand);
        }

        public void CadastrarDocumento(string codigoConvite)
        {
            var cadastrarDocumentoCommand = new CadastrarDocumentoCommand(codigoConvite);
            _bus.SendCommand(cadastrarDocumentoCommand);
        }

        public void CadastrarEmail(string codigoConvite)
        {
            var cadastrarEmailCommand = new CadastrarEmailCommand(codigoConvite);
            _bus.SendCommand(cadastrarEmailCommand);
        }

        public async Task<object> ObterEndereco(string codigoConvite)
        {
            var obterEnderecoCommand = new ObterEnderecoCommand(codigoConvite);
            var obterEnderecoResponse = await _bus.SendCommand(obterEnderecoCommand);

            return _notifications.HasNotifications()
                ? obterEnderecoResponse
                : new List<EnderecoViewModel>
                {
                    new EnderecoViewModel
                    {
                        Id = Guid.NewGuid(),
                        Cep = "34006-086",
                        Logradouro = "Rua da Mata",
                        Numero = 185,
                        Complemento = "APT 1502 T2",
                        Bairro = "Vila da Serra",
                        Cidade = "Nova Lima",
                        Estado = "MG",
                        Principal = true
                    },
                    new EnderecoViewModel
                    {
                        Id = Guid.NewGuid(),
                        Cep = "31540-600",
                        Logradouro = "Rua França",
                        Numero = 155,
                        Complemento = "",
                        Bairro = "Jardim Leblon",
                        Cidade = "Belo Horizonte",
                        Estado = "MG",
                        Principal = false
                    }
                };
        }

        public async Task<object> ObterEnderecoPorId(string codigoConvite, Guid enderecoId)
        {
            var obterEnderecoPorIdCommand = new ObterEnderecoPorIdCommand(codigoConvite, enderecoId);
            var obterEnderecoPorIdResponse = await _bus.SendCommand(obterEnderecoPorIdCommand);

            return _notifications.HasNotifications()
                ? obterEnderecoPorIdResponse
                : new EnderecoViewModel
                {
                    Id = Guid.NewGuid(),
                    Cep = "34006-086",
                    Logradouro = "Rua da Mata",
                    Numero = 185,
                    Complemento = "APT 1502 T2",
                    Bairro = "Vila da Serra",
                    Cidade = "Nova Lima",
                    Estado = "MG"
                };
        }

        public void CadastrarEndereco(string codigoConvite)
        {
            var cadastrarEnderecoCommand = new CadastrarEnderecoCommand(codigoConvite);
            _bus.SendCommand(cadastrarEnderecoCommand);
        }

        public void DeletarEndereco(string codigoConvite, Guid enderecoId)
        {
            var deletarEnderecoCommand = new DeletarEnderecoCommand(codigoConvite, enderecoId);
            _bus.SendCommand(deletarEnderecoCommand);
        }

        public void CadastrarPessoaJuridica(string codigoConvite)
        {
            var cadastrarPessoaJuridicaCommand = new CadastrarPessoaJuridicaCommand(codigoConvite);
            _bus.SendCommand(cadastrarPessoaJuridicaCommand);
        }

        public void CadastrarRepresentante(string codigoConvite)
        {
            var cadastrarRepresentanteCommand = new CadastrarRepresentanteCommand(codigoConvite);
            _bus.SendCommand(cadastrarRepresentanteCommand);
        }

        public void CadastrarTelefone(string codigoConvite)
        {
            var cadastrarTelefoneCommand = new CadastrarTelefoneCommand(codigoConvite);
            _bus.SendCommand(cadastrarTelefoneCommand);
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