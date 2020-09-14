using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using VilevePay.Application.Interfaces;
using VilevePay.Application.Services;
using VilevePay.Domain.CommandHandlers;
using VilevePay.Domain.Commands.Autorizacao;
using VilevePay.Domain.Commands.Cliente;
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
            services.AddScoped<IRequestHandler<ValidarCodigoConviteCommand, bool>, AutorizacaoCommandHandler>();
            services.AddScoped<IRequestHandler<ValidarCodigoTokenCommand, bool>, AutorizacaoCommandHandler>();
            services.AddScoped<IRequestHandler<ValidarEmailCommand, bool>, AutorizacaoCommandHandler>();
            services.AddScoped<IRequestHandler<EnviarSmsTokenCommand, bool>, AutorizacaoCommandHandler>();
            services.AddScoped<IRequestHandler<EnviarVerificadorEmailCommand, bool>, AutorizacaoCommandHandler>();

            // Cliente
            services.AddScoped<IRequestHandler<ValidarPessoaFisicaCommand, bool>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<RegistrarComprovantePessoaFisicaCommand, bool>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<ValidarPessoaJuridicaCommand, bool>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<RegistrarComprovantePessoaJuridicaCommand, bool>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<RegistrarEnderecoCommand, bool>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<RegistrarComprovanteEnderecoCommand, bool>, ClienteCommandHandler>();

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