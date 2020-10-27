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

            return _notifications.HasNotifications()
                ? obterEstadoCivilResponse
                : new List<EstadoCivilViewModel>
                {
                    new EstadoCivilViewModel
                    {
                        CodigoEstadoCivil = 1,
                        Nome = "Solteiro"
                    },
                    new EstadoCivilViewModel
                    {
                        CodigoEstadoCivil = 2,
                        Nome = "Casado"
                    },
                    new EstadoCivilViewModel
                    {
                        CodigoEstadoCivil = 3,
                        Nome = "Separado"
                    },
                    new EstadoCivilViewModel
                    {
                        CodigoEstadoCivil = 4,
                        Nome = "Divorciado"
                    },
                    new EstadoCivilViewModel
                    {
                        CodigoEstadoCivil = 5,
                        Nome = "Viúvo"
                    }
                };
        }

        public async Task<object> ObterNacionalidade()
        {
            {
                var obterNacionalidadeCommand = new ObterNacionalidadeCommand();
                var obterNacionalidadeResponse = await _bus.SendCommand(obterNacionalidadeCommand);

                return _notifications.HasNotifications()
                    ? obterNacionalidadeResponse
                    : new List<NacionalidadeViewModel>
                    {
                        new NacionalidadeViewModel
                        {
                            CodigoNacionalidade = 1,
                            Nome = "Afeganistão",
                            PrefixoPais = "DAF"
                        },
                        new NacionalidadeViewModel
                        {
                            CodigoNacionalidade = 2,
                            Nome = "África do Sul",
                            PrefixoPais = "DZA"
                        },
                        new NacionalidadeViewModel
                        {
                            CodigoNacionalidade = 3,
                            Nome = "Albânia",
                            PrefixoPais = "DAL"
                        },
                        new NacionalidadeViewModel
                        {
                            CodigoNacionalidade = 4,
                            Nome = "Alemanha",
                            PrefixoPais = "DDE"
                        },
                        new NacionalidadeViewModel
                        {
                            CodigoNacionalidade = 5,
                            Nome = "Andorra",
                            PrefixoPais = "DAD"
                        }
                    };
            }
        }

        public async Task<object> ObterPerfilUsuario()
        {
            var obterPerfilUsuarioCommand = new ObterPerfilUsuarioCommand();
            var obterPerfilUsuarioResponse = await _bus.SendCommand(obterPerfilUsuarioCommand);

            return _notifications.HasNotifications()
                ? obterPerfilUsuarioResponse
                : new List<PerfilUsuarioViewModel>
                {
                    new PerfilUsuarioViewModel
                    {
                        CodigoPerfil = 17,
                        Nome = "Parceiro"
                    }
                };
        }

        public async Task<object> ObterTipoTelefone()
        {
            var obterTipoTelefoneCommand = new ObterTipoTelefoneCommand();
            var obterTipoTelefoneResponse = await _bus.SendCommand(obterTipoTelefoneCommand);

            return _notifications.HasNotifications()
                ? obterTipoTelefoneResponse
                : new List<TipoTelefoneViewModel>
                {
                    new TipoTelefoneViewModel
                    {
                        TipoTelefone = 1,
                        Nome = "Residencial"
                    },
                    new TipoTelefoneViewModel
                    {
                        TipoTelefone = 2,
                        Nome = "Celular"
                    },
                    new TipoTelefoneViewModel
                    {
                        TipoTelefone = 3,
                        Nome = "Fax"
                    },
                    new TipoTelefoneViewModel
                    {
                        TipoTelefone = 4,
                        Nome = "Corporativo"
                    }
                };
        }

        public async Task<object> ObterTipoEmail()
        {
            var obterTipoEmailCommand = new ObterTipoEmailCommand();
            var obterTipoEmailResponse = await _bus.SendCommand(obterTipoEmailCommand);

            return _notifications.HasNotifications()
                ? obterTipoEmailResponse
                : new List<TipoEmailViewModel>
                {
                    new TipoEmailViewModel
                    {
                        TipoEmail = 1,
                        Nome = "Pessoal"
                    },
                    new TipoEmailViewModel
                    {
                        TipoEmail = 2,
                        Nome = "Comercial"
                    }
                };
        }

        public async Task<object> ObterTipoEndereco()
        {
            var obterTipoEnderecoCommand = new ObterTipoEnderecoCommand();
            var obterTipoEnderecoResponse = await _bus.SendCommand(obterTipoEnderecoCommand);

            return _notifications.HasNotifications()
                ? obterTipoEnderecoResponse
                : new List<TipoEnderecoViewModel>
                {
                    new TipoEnderecoViewModel
                    {
                        TipoEndereco = 0,
                        Nome = "Residencial"
                    },
                    new TipoEnderecoViewModel
                    {
                        TipoEndereco = 1,
                        Nome = "Comercial"
                    }
                };
        }

        public async Task<object> ObterBanco()
        {
            var obterBancoCommand = new ObterBancoCommand();
            var obterBancoResponse = await _bus.SendCommand(obterBancoCommand);

            return _notifications.HasNotifications()
                ? obterBancoResponse
                : new List<BancoViewModel>
                {
                    new BancoViewModel
                    {
                        CodigoBanco = 39,
                        Nome = "Caixa"
                    },
                    new BancoViewModel
                    {
                        CodigoBanco = 40,
                        Nome = "Bradesco"
                    },
                    new BancoViewModel
                    {
                        CodigoBanco = 41,
                        Nome = "Santander"
                    },
                    new BancoViewModel
                    {
                        CodigoBanco = 42,
                        Nome = "Itau"
                    }
                };
        }

        public async Task<object> ObterOperacaoBancaria()
        {
            var obterOperacaoBancariaCommand = new ObterOperacaoBancariaCommand();
            var obterOperacaoBancariaResponse = await _bus.SendCommand(obterOperacaoBancariaCommand);

            return _notifications.HasNotifications()
                ? obterOperacaoBancariaResponse
                : new List<OperacaoBancariaViewModel>
                {
                    new OperacaoBancariaViewModel
                    {
                        CodigoOperacao = 1,
                        Tipo = "013",
                        Nome = "Poupança / Física"
                    },
                    new OperacaoBancariaViewModel
                    {
                        CodigoOperacao = 2,
                        Tipo = "SEM",
                        Nome = "NULL"
                    },
                    new OperacaoBancariaViewModel
                    {
                        CodigoOperacao = 3,
                        Tipo = "023",
                        Nome = "Conta Caixa Fácil"
                    },
                    new OperacaoBancariaViewModel
                    {
                        CodigoOperacao = 5,
                        Tipo = "001",
                        Nome = "Conta Corrente / Física"
                    }
                };
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
                        CodigoSexo = 1,
                        Nome = "Feminino"
                    },
                    new SexoViewModel
                    {
                        CodigoSexo = 2,
                        Nome = "Masculino"
                    },
                    new SexoViewModel
                    {
                        CodigoSexo = 3,
                        Nome = "Não declarado"
                    }
                };
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}