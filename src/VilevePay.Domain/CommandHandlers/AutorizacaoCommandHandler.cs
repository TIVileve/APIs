﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VilevePay.Domain.Commands.Autorizacao;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;
using VilevePay.Domain.Interfaces;
using VilevePay.Domain.Models;

namespace VilevePay.Domain.CommandHandlers
{
    public class AutorizacaoCommandHandler : CommandHandler,
        IRequestHandler<LoginCommand, object>,
        IRequestHandler<CadastrarSenhaCommand, bool>,
        IRequestHandler<ValidarCodigoConviteCommand, bool>,
        IRequestHandler<ValidarTokenSmsCommand, bool>,
        IRequestHandler<ValidarTokenEmailCommand, bool>,
        IRequestHandler<EnviarTokenSmsCommand, bool>,
        IRequestHandler<EnviarTokenEmailCommand, bool>,
        IRequestHandler<ValidarSelfieCommand, bool>
    {
        private readonly IOnboardingRepository _onboardingRepository;

        public AutorizacaoCommandHandler(
            IOnboardingRepository onboardingRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _onboardingRepository = onboardingRepository;
        }

        public async Task<object> Handle(LoginCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public Task<bool> Handle(CadastrarSenhaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(ValidarCodigoConviteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            // API Vileve

            var onboarding = _onboardingRepository.Find(o => o.CodigoConvite.Equals(message.CodigoConvite)).FirstOrDefault();
            if (onboarding != null)
                return Task.FromResult(true);

            onboarding = new Onboarding(Guid.NewGuid())
            {
                CodigoConvite = message.CodigoConvite
            };

            _onboardingRepository.Add(onboarding);

            if (Commit())
            {
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(ValidarTokenSmsCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(ValidarTokenEmailCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(EnviarTokenSmsCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(EnviarTokenEmailCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(ValidarSelfieCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}