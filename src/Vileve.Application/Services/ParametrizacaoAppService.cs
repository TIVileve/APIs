using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Vileve.Application.Interfaces;
using Vileve.Application.ViewModels.v1.Parametrizacao;
using Vileve.Domain.Commands.Parametrizacao;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Notifications;
using Vileve.Domain.Responses;

namespace Vileve.Application.Services
{
    public class ParametrizacaoAppService : IParametrizacaoAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;

        public ParametrizacaoAppService(
            IMapper mapper,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
        {
            _mapper = mapper;
            _bus = bus;
            _notifications = (DomainNotificationHandler)notifications;
        }

        public async Task<object> ObterEstadoCivil()
        {
            var obterEstadoCivilCommand = new ObterEstadoCivilCommand();
            var obterEstadoCivilResponse = await _bus.SendCommand(obterEstadoCivilCommand);

            return _notifications.HasNotifications() ? obterEstadoCivilResponse : _mapper.Map<IEnumerable<EstadoCivilViewModel>>((IEnumerable<ParametrizacaoEstadoCivil>)obterEstadoCivilResponse);
        }

        public async Task<object> ObterNacionalidade()
        {
            {
                var obterNacionalidadeCommand = new ObterNacionalidadeCommand();
                var obterNacionalidadeResponse = await _bus.SendCommand(obterNacionalidadeCommand);

                return _notifications.HasNotifications() ? obterNacionalidadeResponse : _mapper.Map<IEnumerable<NacionalidadeViewModel>>((IEnumerable<ParametrizacaoNacionalidade>)obterNacionalidadeResponse);
            }
        }

        public async Task<object> ObterPerfilUsuario()
        {
            var obterPerfilUsuarioCommand = new ObterPerfilUsuarioCommand();
            var obterPerfilUsuarioResponse = await _bus.SendCommand(obterPerfilUsuarioCommand);

            return _notifications.HasNotifications() ? obterPerfilUsuarioResponse : _mapper.Map<IEnumerable<PerfilUsuarioViewModel>>((IEnumerable<ParametrizacaoPerfilUsuario>)obterPerfilUsuarioResponse);
        }

        public async Task<object> ObterTipoTelefone()
        {
            var obterTipoTelefoneCommand = new ObterTipoTelefoneCommand();
            var obterTipoTelefoneResponse = await _bus.SendCommand(obterTipoTelefoneCommand);

            return _notifications.HasNotifications() ? obterTipoTelefoneResponse : _mapper.Map<IEnumerable<TipoTelefoneViewModel>>((IEnumerable<ParametrizacaoTipoTelefone>)obterTipoTelefoneResponse);
        }

        public async Task<object> ObterTipoEmail()
        {
            var obterTipoEmailCommand = new ObterTipoEmailCommand();
            var obterTipoEmailResponse = await _bus.SendCommand(obterTipoEmailCommand);

            return _notifications.HasNotifications() ? obterTipoEmailResponse : _mapper.Map<IEnumerable<TipoEmailViewModel>>((IEnumerable<ParametrizacaoTipoEmail>)obterTipoEmailResponse);
        }

        public async Task<object> ObterTipoEndereco()
        {
            var obterTipoEnderecoCommand = new ObterTipoEnderecoCommand();
            var obterTipoEnderecoResponse = await _bus.SendCommand(obterTipoEnderecoCommand);

            return _notifications.HasNotifications() ? obterTipoEnderecoResponse : _mapper.Map<IEnumerable<TipoEnderecoViewModel>>((IEnumerable<ParametrizacaoTipoEndereco>)obterTipoEnderecoResponse);
        }

        public async Task<object> ObterBanco()
        {
            var obterBancoCommand = new ObterBancoCommand();
            var obterBancoResponse = await _bus.SendCommand(obterBancoCommand);

            return _notifications.HasNotifications() ? obterBancoResponse : _mapper.Map<IEnumerable<BancoViewModel>>((IEnumerable<ParametrizacaoBanco>)obterBancoResponse);
        }

        public async Task<object> ObterOperacaoBancaria()
        {
            var obterOperacaoBancariaCommand = new ObterOperacaoBancariaCommand();
            var obterOperacaoBancariaResponse = await _bus.SendCommand(obterOperacaoBancariaCommand);

            return _notifications.HasNotifications() ? obterOperacaoBancariaResponse : _mapper.Map<IEnumerable<OperacaoBancariaViewModel>>((IEnumerable<ParametrizacaoOperacaoBancaria>)obterOperacaoBancariaResponse);
        }

        public async Task<object> ObterSexo()
        {
            var obterSexoCommand = new ObterSexoCommand();
            var obterSexoResponse = await _bus.SendCommand(obterSexoCommand);

            return _notifications.HasNotifications() ? obterSexoResponse : _mapper.Map<IEnumerable<SexoViewModel>>((IEnumerable<ParametrizacaoSexo>)obterSexoResponse);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}