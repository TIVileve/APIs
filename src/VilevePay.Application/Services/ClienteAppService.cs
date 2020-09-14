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

        public void ValidarPessoaFisicaCommand(string codigoConvite, string cpf)
        {
            var validarPessoaFisicaCommand = new ValidarPessoaFisicaCommand(codigoConvite, cpf);
            _bus.SendCommand(validarPessoaFisicaCommand);
        }

        public void RegistrarComprovantePessoaFisicaCommand(string codigoConvite, string comprovanteBase64)
        {
            var registrarComprovantePessoaFisicaCommand = new RegistrarComprovantePessoaFisicaCommand(codigoConvite, comprovanteBase64);
            _bus.SendCommand(registrarComprovantePessoaFisicaCommand);
        }

        public void ValidarPessoaJuridicaCommand(string codigoConvite, string cnpj)
        {
            var validarPessoaJuridicaCommand = new ValidarPessoaJuridicaCommand(codigoConvite, cnpj);
            _bus.SendCommand(validarPessoaJuridicaCommand);
        }

        public void RegistrarComprovantePessoaJuridicaCommand(string codigoConvite, string comprovanteBase64)
        {
            var registrarComprovantePessoaJuridicaCommand = new RegistrarComprovantePessoaJuridicaCommand(codigoConvite, comprovanteBase64);
            _bus.SendCommand(registrarComprovantePessoaJuridicaCommand);
        }

        public void RegistrarEnderecoCommand(string codigoConvite, string cep, string logradouro, int numero, string complemento, string bairro, string cidade, string estado)
        {
            var registrarEnderecoCommand = new RegistrarEnderecoCommand(codigoConvite, cep, logradouro, numero, complemento, bairro, cidade, estado);
            _bus.SendCommand(registrarEnderecoCommand);
        }

        public void RegistrarComprovanteEnderecoCommand(string codigoConvite, string comprovanteEnderecoBase64)
        {
            var registrarComprovanteEnderecoCommand = new RegistrarComprovanteEnderecoCommand(codigoConvite, comprovanteEnderecoBase64);
            _bus.SendCommand(registrarComprovanteEnderecoCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}