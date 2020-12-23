using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VilevePay.Application.Interfaces;
using VilevePay.Application.ViewModels.v1.Parametrizacao;
using VilevePay.Domain.Commands.Parametrizacao;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;
using VilevePay.Domain.Responses;

namespace VilevePay.Application.Services
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

            return _notifications.HasNotifications()
                ? obterSexoResponse
                : new List<SexoViewModel>
                {
                    new SexoViewModel
                    {
                        CodigoSexo = 0,
                        Nome = "Feminino"
                    },
                    new SexoViewModel
                    {
                        CodigoSexo = 1,
                        Nome = "Masculino"
                    },
                    new SexoViewModel
                    {
                        CodigoSexo = 2,
                        Nome = "Não declarado"
                    }
                };
        }

        public async Task<object> ObterTipoParentesco()
        {
            var obterTipoParentescoCommand = new ObterTipoParentescoCommand();
            var obterTipoParentescoResponse = await _bus.SendCommand(obterTipoParentescoCommand);

            return _notifications.HasNotifications()
                ? obterTipoParentescoResponse
                : new List<ParentescoViewModel>
                {
                    new ParentescoViewModel
                    {
                        CodigoParentesco = 0,
                        Nome = "Pai"
                    },
                    new ParentescoViewModel
                    {
                        CodigoParentesco = 1,
                        Nome = "Sogro"
                    },
                    new ParentescoViewModel
                    {
                        CodigoParentesco = 2,
                        Nome = "Irmão"
                    }
                };
        }

        public async Task<object> ObterTipoPagamento()
        {
            var obterTipoPagamentoCommand = new ObterTipoPagamentoCommand();
            var obterTipoPagamentoResponse = await _bus.SendCommand(obterTipoPagamentoCommand);

            return _notifications.HasNotifications()
                ? obterTipoPagamentoResponse
                : new List<PagamentoViewModel>
                {
                    new PagamentoViewModel
                    {
                        CodigoPagamento = 0,
                        Nome = "Cartão de crédito"
                    },
                    new PagamentoViewModel
                    {
                        CodigoPagamento = 1,
                        Nome = "Boleto"
                    },
                    new PagamentoViewModel
                    {
                        CodigoPagamento = 2,
                        Nome = "Débito em conta"
                    },
                    new PagamentoViewModel
                    {
                        CodigoPagamento = 3,
                        Nome = "Convênio"
                    }
                };
        }

        public async Task<object> ObterTipoConvenio()
        {
            var obterTipoConvenioCommand = new ObterTipoConvenioCommand();
            var obterTipoConvenioResponse = await _bus.SendCommand(obterTipoConvenioCommand);

            return _notifications.HasNotifications()
                ? obterTipoConvenioResponse
                : new List<ConvenioViewModel>
                {
                    new ConvenioViewModel
                    {
                        CodigoConvenio = 0,
                        Nome = "Metropax"
                    },
                    new ConvenioViewModel
                    {
                        CodigoConvenio = 1,
                        Nome = "Promed"
                    },
                    new ConvenioViewModel
                    {
                        CodigoConvenio = 2,
                        Nome = "Vitallis"
                    }
                };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}