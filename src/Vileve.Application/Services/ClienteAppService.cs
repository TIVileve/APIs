using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Vileve.Application.Interfaces;
using Vileve.Application.ViewModels.v1.Cliente;
using Vileve.Domain.Commands.Cliente;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Notifications;
using Vileve.Domain.Models;
using Vileve.Domain.Responses;

namespace Vileve.Application.Services
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

        public async Task<object> ObterClientePorId(Guid clienteId)
        {
            var obterClientePorIdCommand = new ObterClientePorIdCommand(clienteId);
            var obterClientePorIdResponse = await _bus.SendCommand(obterClientePorIdCommand);

            return _notifications.HasNotifications() ? obterClientePorIdResponse : _mapper.Map<ClienteViewModel>((Cliente)obterClientePorIdResponse);
        }

        public async Task<object> CadastrarCliente(string cpf, string nomeCompleto, DateTime dataNascimento, string email,
            string telefoneFixo, string telefoneCelular, Guid? consultorId)
        {
            var cadastrarClienteCommand = new CadastrarClienteCommand(cpf, nomeCompleto, dataNascimento, email,
                telefoneFixo, telefoneCelular, consultorId);
            var cadastrarClienteResponse = await _bus.SendCommand(cadastrarClienteCommand);

            return _notifications.HasNotifications() ? cadastrarClienteResponse : _mapper.Map<ClienteViewModel>((Cliente)cadastrarClienteResponse);
        }

        public async Task<object> ObterProduto()
        {
            var obterProdutoCommand = new ObterProdutoCommand();
            var obterProdutoResponse = await _bus.SendCommand(obterProdutoCommand);

            return _notifications.HasNotifications() ? obterProdutoResponse : _mapper.Map<ProdutoViewModel>((SeguroProduto)obterProdutoResponse);
        }

        public void CadastrarProduto(Guid clienteId, string codigoProdutoItem)
        {
            var cadastrarProdutoCommand = new CadastrarProdutoCommand(clienteId, codigoProdutoItem);
            _bus.SendCommand(cadastrarProdutoCommand);
        }

        public void CadastrarEndereco(Guid clienteId, string cep, string logradouro, int numero, string complemento,
            string bairro, string cidade, string estado, string comprovanteBase64)
        {
            var cadastrarEnderecoCommand = new CadastrarEnderecoCommand(clienteId, cep, logradouro, numero, complemento,
                bairro, cidade, estado, comprovanteBase64);
            _bus.SendCommand(cadastrarEnderecoCommand);
        }

        public async Task<object> ObterDependente(Guid clienteId)
        {
            var obterDependenteCommand = new ObterDependenteCommand(clienteId);
            var obterDependenteResponse = await _bus.SendCommand(obterDependenteCommand);

            return _notifications.HasNotifications() ? obterDependenteResponse : _mapper.Map<IEnumerable<DependenteViewModel>>((IEnumerable<ClienteDependente>)obterDependenteResponse);
        }

        public async Task<object> ObterDependentePorId(Guid clienteId, Guid dependenteId)
        {
            var obterDependentePorIdCommand = new ObterDependentePorIdCommand(clienteId, dependenteId);
            var obterDependentePorIdResponse = await _bus.SendCommand(obterDependentePorIdCommand);

            return _notifications.HasNotifications() ? obterDependentePorIdResponse : _mapper.Map<DependenteViewModel>((ClienteDependente)obterDependentePorIdResponse);
        }

        public void CadastrarDependente(Guid clienteId, string codigoParentesco, string nomeCompleto, DateTime dataNascimento, string cpf,
            string email, string telefoneCelular, string cep, string logradouro, int numero,
            string complemento, string bairro, string cidade, string estado)
        {
            var cadastrarDependenteCommand = new CadastrarDependenteCommand(clienteId, codigoParentesco, nomeCompleto, dataNascimento, cpf,
                email, telefoneCelular, cep, logradouro, numero,
                complemento, bairro, cidade, estado);
            _bus.SendCommand(cadastrarDependenteCommand);
        }

        public void AtualizarDependente(Guid clienteId, Guid dependenteId, string codigoParentesco, string nomeCompleto, DateTime dataNascimento, string cpf,
            string email, string telefoneCelular, string cep, string logradouro, int numero,
            string complemento, string bairro, string cidade, string estado)
        {
            var atualizarDependenteCommand = new AtualizarDependenteCommand(clienteId, dependenteId, codigoParentesco, nomeCompleto, dataNascimento, cpf,
                email, telefoneCelular, cep, logradouro, numero,
                complemento, bairro, cidade, estado);
            _bus.SendCommand(atualizarDependenteCommand);
        }

        public void DeletarDependente(Guid clienteId, Guid dependenteId)
        {
            var deletarDependenteCommand = new DeletarDependenteCommand(clienteId, dependenteId);
            _bus.SendCommand(deletarDependenteCommand);
        }

        public void CadastrarPagamento(Guid clienteId)
        {
            var cadastrarPagamentoCommand = new CadastrarPagamentoCommand(clienteId);
            _bus.SendCommand(cadastrarPagamentoCommand);
        }

        public async Task<object> ObterCalculoMensal(Guid clienteId)
        {
            var obterCalculoMensalCommand = new ObterCalculoMensalCommand(clienteId);
            var obterCalculoMensalResponse = await _bus.SendCommand(obterCalculoMensalCommand);

            return _notifications.HasNotifications()
                ? obterCalculoMensalResponse
                : new CalculoMensalViewModel
                {
                    Valor = new Random().Next(1500, 3000)
                };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}