using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VilevePay.Domain.Commands.Consultor;
using VilevePay.Domain.Core.Bus;
using VilevePay.Domain.Core.Notifications;
using VilevePay.Domain.Enums;
using VilevePay.Domain.Interfaces;
using VilevePay.Domain.Models;

namespace VilevePay.Domain.CommandHandlers
{
    public class ConsultorCommandHandler : CommandHandler,
        IRequestHandler<ObterEnderecoCommand, object>,
        IRequestHandler<ObterEnderecoPorIdCommand, object>,
        IRequestHandler<CadastrarEnderecoCommand, bool>,
        IRequestHandler<DeletarEnderecoCommand, bool>,
        IRequestHandler<CadastrarPessoaJuridicaCommand, bool>,
        IRequestHandler<CadastrarRepresentanteCommand, bool>,
        IRequestHandler<ObterStatusOnboardingCommand, object>
    {
        private readonly IOnboardingRepository _onboardingRepository;
        private readonly IConsultorRepository _consultorRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public ConsultorCommandHandler(
            IOnboardingRepository onboardingRepository,
            IConsultorRepository consultorRepository,
            IEnderecoRepository enderecoRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _onboardingRepository = onboardingRepository;
            _consultorRepository = consultorRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task<object> Handle(ObterEnderecoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public async Task<object> Handle(ObterEnderecoPorIdCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            var onboarding = _onboardingRepository.Find(o => o.CodigoConvite.Equals(message.CodigoConvite)).FirstOrDefault();
            if (onboarding == null)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Código do convite não encontrado."));
                return await Task.FromResult(false);
            }

            var endereco = _enderecoRepository.GetById(message.EnderecoId);
            if (endereco != null)
                return await Task.FromResult(endereco);

            await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Endereço não encontrado."));
            return await Task.FromResult(false);
        }

        public Task<bool> Handle(CadastrarEnderecoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var onboarding = _onboardingRepository.Find(o => o.CodigoConvite.Equals(message.CodigoConvite)).FirstOrDefault();
            if (onboarding == null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Código do convite não encontrado."));
                return Task.FromResult(false);
            }

            if (onboarding.Consultor == null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Consultor não cadastrado."));
                return Task.FromResult(false);
            }

            var endereco = new Endereco(Guid.NewGuid(), (TipoEndereco)message.TipoEndereco, message.Cep, message.Logradouro, message.Numero,
                message.Complemento, message.Bairro, message.Cidade, message.Estado, message.Principal,
                message.ComprovanteBase64, onboarding.Consultor.Id);

            _enderecoRepository.Add(endereco);

            if (Commit())
            {
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(DeletarEnderecoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var onboarding = _onboardingRepository.Find(o => o.CodigoConvite.Equals(message.CodigoConvite)).FirstOrDefault();
            if (onboarding == null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Código do convite não encontrado."));
                return Task.FromResult(false);
            }

            _enderecoRepository.Remove(message.EnderecoId);

            if (Commit())
            {
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(CadastrarPessoaJuridicaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var onboarding = _onboardingRepository.Find(o => o.CodigoConvite.Equals(message.CodigoConvite)).FirstOrDefault();
            if (onboarding == null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Código do convite não encontrado."));
                return Task.FromResult(false);
            }

            var consultor = new Consultor(Guid.NewGuid())
            {
                OnboardingId = onboarding.Id
            };

            _consultorRepository.Add(consultor);

            if (Commit())
            {
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(CadastrarRepresentanteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public async Task<object> Handle(ObterStatusOnboardingCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }
    }
}