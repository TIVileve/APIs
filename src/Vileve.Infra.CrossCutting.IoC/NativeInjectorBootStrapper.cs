using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Vileve.Application.Interfaces;
using Vileve.Application.Services;
using Vileve.Domain.CommandHandlers;
using Vileve.Domain.Commands.Autorizacao;
using Vileve.Domain.Commands.Cliente;
using Vileve.Domain.Commands.Consultor;
using Vileve.Domain.Commands.Parametrizacao;
using Vileve.Domain.Commands.Property;
using Vileve.Domain.Core.Bus;
using Vileve.Domain.Core.Events;
using Vileve.Domain.Core.Notifications;
using Vileve.Domain.EventHandlers;
using Vileve.Domain.Events.Property;
using Vileve.Domain.Interfaces;
using Vileve.Infra.CrossCutting.Bus;
using Vileve.Infra.CrossCutting.Identity.Authorization;
using Vileve.Infra.CrossCutting.Identity.Models;
using Vileve.Infra.CrossCutting.Io.Http;
using Vileve.Infra.Data.Context;
using Vileve.Infra.Data.EventSourcing;
using Vileve.Infra.Data.Repository;
using Vileve.Infra.Data.Repository.EventSourcing;
using Vileve.Infra.Data.UoW;
using CadastrarEnderecoCommand = Vileve.Domain.Commands.Consultor.CadastrarEnderecoCommand;
using ObterEnderecoCommand = Vileve.Domain.Commands.Endereco.ObterEnderecoCommand;

namespace Vileve.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // ASP.NET Authorization Polices
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            // Application
            services.AddScoped<IPropertyAppService, PropertyAppService>();
            services.AddScoped<IAutorizacaoAppService, AutorizacaoAppService>();
            services.AddScoped<IEnderecoAppService, EnderecoAppService>();
            services.AddScoped<IParametrizacaoAppService, ParametrizacaoAppService>();
            services.AddScoped<IConsultorAppService, ConsultorAppService>();
            services.AddScoped<IClienteAppService, ClienteAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Property
            services.AddScoped<INotificationHandler<PropertyRegisteredEvent>, PropertyEventHandler>();
            services.AddScoped<INotificationHandler<PropertyUpdatedEvent>, PropertyEventHandler>();
            services.AddScoped<INotificationHandler<PropertyRemovedEvent>, PropertyEventHandler>();

            // Domain - Commands

            // Property
            services.AddScoped<IRequestHandler<RegisterNewPropertyCommand, object>, PropertyCommandHandler>();
            services.AddScoped<IRequestHandler<UpdatePropertyCommand, bool>, PropertyCommandHandler>();
            services.AddScoped<IRequestHandler<RemovePropertyCommand, bool>, PropertyCommandHandler>();

            // Autorizacao
            services.AddScoped<IRequestHandler<LoginCommand, object>, AutorizacaoCommandHandler>();
            services.AddScoped<IRequestHandler<CadastrarSenhaCommand, bool>, AutorizacaoCommandHandler>();
            services.AddScoped<IRequestHandler<ValidarCodigoConviteCommand, bool>, AutorizacaoCommandHandler>();
            services.AddScoped<IRequestHandler<ValidarTokenSmsCommand, bool>, AutorizacaoCommandHandler>();
            services.AddScoped<IRequestHandler<ValidarTokenEmailCommand, bool>, AutorizacaoCommandHandler>();
            services.AddScoped<IRequestHandler<EnviarTokenSmsCommand, bool>, AutorizacaoCommandHandler>();
            services.AddScoped<IRequestHandler<EnviarTokenEmailCommand, bool>, AutorizacaoCommandHandler>();
            services.AddScoped<IRequestHandler<ValidarSelfieCommand, bool>, AutorizacaoCommandHandler>();

            // Endereco
            services.AddScoped<IRequestHandler<ObterEnderecoCommand, object>, EnderecoCommandHandler>();

            // Parametrizacao
            services.AddScoped<IRequestHandler<ObterEstadoCivilCommand, object>, ParametrizacaoCommandHandler>();
            services.AddScoped<IRequestHandler<ObterNacionalidadeCommand, object>, ParametrizacaoCommandHandler>();
            services.AddScoped<IRequestHandler<ObterPerfilUsuarioCommand, object>, ParametrizacaoCommandHandler>();
            services.AddScoped<IRequestHandler<ObterTipoTelefoneCommand, object>, ParametrizacaoCommandHandler>();
            services.AddScoped<IRequestHandler<ObterTipoEmailCommand, object>, ParametrizacaoCommandHandler>();
            services.AddScoped<IRequestHandler<ObterTipoEnderecoCommand, object>, ParametrizacaoCommandHandler>();
            services.AddScoped<IRequestHandler<ObterBancoCommand, object>, ParametrizacaoCommandHandler>();
            services.AddScoped<IRequestHandler<ObterOperacaoBancariaCommand, object>, ParametrizacaoCommandHandler>();
            services.AddScoped<IRequestHandler<ObterSexoCommand, object>, ParametrizacaoCommandHandler>();

            // Consultor
            services.AddScoped<IRequestHandler<Domain.Commands.Consultor.ObterEnderecoCommand, object>, ConsultorCommandHandler>();
            services.AddScoped<IRequestHandler<ObterEnderecoPorIdCommand, object>, ConsultorCommandHandler>();
            services.AddScoped<IRequestHandler<CadastrarEnderecoCommand, bool>, ConsultorCommandHandler>();
            services.AddScoped<IRequestHandler<DeletarEnderecoCommand, bool>, ConsultorCommandHandler>();
            services.AddScoped<IRequestHandler<CadastrarPessoaJuridicaCommand, bool>, ConsultorCommandHandler>();
            services.AddScoped<IRequestHandler<CadastrarRepresentanteCommand, bool>, ConsultorCommandHandler>();
            services.AddScoped<IRequestHandler<ObterStatusOnboardingCommand, object>, ConsultorCommandHandler>();
            services.AddScoped<IRequestHandler<ValidarConsultorCommand, bool>, ConsultorCommandHandler>();
            services.AddScoped<IRequestHandler<ValidarPessoaJuridicaCommand, bool>, ConsultorCommandHandler>();
            services.AddScoped<IRequestHandler<ValidarRepresentanteCommand, bool>, ConsultorCommandHandler>();

            // Cliente
            services.AddScoped<IRequestHandler<ObterClientePorIdCommand, object>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<CadastrarClienteCommand, object>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarClienteCommand, bool>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<ObterProdutoCommand, object>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<CadastrarProdutoCommand, bool>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<Domain.Commands.Cliente.CadastrarEnderecoCommand, bool>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarEnderecoCommand, bool>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<ObterDependenteCommand, object>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<ObterDependentePorIdCommand, object>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<CadastrarDependenteCommand, bool>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarDependenteCommand, bool>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<DeletarDependenteCommand, bool>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<ContratarProdutoCommand, object>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<CadastrarPagamentoCommand, bool>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<CadastrarDocumentoCommand, bool>, ClienteCommandHandler>();

            // Infra - Data
            services.AddScoped<IOnboardingRepository, OnboardingRepository>();
            services.AddScoped<IConsultorRepository, ConsultorRepository>();
            services.AddScoped<IDadosBancariosRepository, DadosBancariosRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IRepresentanteRepository, RepresentanteRepository>();
            services.AddScoped<IRepresentanteEmailRepository, RepresentanteEmailRepository>();
            services.AddScoped<IRepresentanteTelefoneRepository, RepresentanteTelefoneRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IClienteFontePagadoraRepository, ClienteFontePagadoraRepository>();
            services.AddScoped<IClienteProdutoRepository, ClienteProdutoRepository>();
            services.AddScoped<IClienteEnderecoRepository, ClienteEnderecoRepository>();
            services.AddScoped<IClienteDependenteRepository, ClienteDependenteRepository>();
            services.AddScoped<IClienteDocumentoRepository, ClienteDocumentoRepository>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<VileveContext>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSqlRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSqlContext>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();

            // Infra - IO
            services.AddScoped<IHttpAppService, HttpAppService>();
        }
    }
}