using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using VilevePay.Application.Interfaces;
using VilevePay.Application.Services;
using VilevePay.Domain.CommandHandlers;
using VilevePay.Domain.Commands.Autorizacao;
using VilevePay.Domain.Commands.Consultor;
using VilevePay.Domain.Commands.Parametrizacao;
using VilevePay.Domain.Commands.Property;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Events;
using VilevePay.Domain.Core.Notifications;
using VilevePay.Domain.EventHandlers;
using VilevePay.Domain.Events.Property;
using VilevePay.Domain.Interfaces;
using VilevePay.Infra.CrossCutting.Bus;
using VilevePay.Infra.CrossCutting.Identity.Authorization;
using VilevePay.Infra.CrossCutting.Identity.Models;
using VilevePay.Infra.Data.Context;
using VilevePay.Infra.Data.EventSourcing;
using VilevePay.Infra.Data.Repository;
using VilevePay.Infra.Data.Repository.EventSourcing;
using VilevePay.Infra.Data.UoW;
using ObterEnderecoCommand = VilevePay.Domain.Commands.Endereco.ObterEnderecoCommand;

namespace VilevePay.Infra.CrossCutting.IoC
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
            services.AddScoped<IRequestHandler<CadastrarArquivoCommand, bool>, ConsultorCommandHandler>();
            services.AddScoped<IRequestHandler<CadastrarDadosBancariosCommand, bool>, ConsultorCommandHandler>();
            services.AddScoped<IRequestHandler<CadastrarDocumentoCommand, bool>, ConsultorCommandHandler>();
            services.AddScoped<IRequestHandler<CadastrarEmailCommand, bool>, ConsultorCommandHandler>();
            services.AddScoped<IRequestHandler<Domain.Commands.Consultor.ObterEnderecoCommand, object>, ConsultorCommandHandler>();
            services.AddScoped<IRequestHandler<CadastrarEnderecoCommand, bool>, ConsultorCommandHandler>();
            services.AddScoped<IRequestHandler<DeletarEnderecoCommand, bool>, ConsultorCommandHandler>();
            services.AddScoped<IRequestHandler<CadastrarPessoaJuridicaCommand, bool>, ConsultorCommandHandler>();
            services.AddScoped<IRequestHandler<CadastrarTelefoneCommand, bool>, ConsultorCommandHandler>();
            services.AddScoped<IRequestHandler<ObterStatusOnboardingCommand, object>, ConsultorCommandHandler>();

            // Infra - Data
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<VilevePayContext>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSqlRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSqlContext>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}