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
        private readonly IDadosBancariosRepository _dadosBancariosRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IRepresentanteRepository _representanteRepository;
        private readonly IRepresentanteEmailRepository _representanteEmailRepository;
        private readonly IRepresentanteTelefoneRepository _representanteTelefoneRepository;

        public ConsultorCommandHandler(
            IOnboardingRepository onboardingRepository,
            IConsultorRepository consultorRepository,
            IDadosBancariosRepository dadosBancariosRepository,
            IEnderecoRepository enderecoRepository,
            IRepresentanteRepository representanteRepository,
            IRepresentanteEmailRepository representanteEmailRepository,
            IRepresentanteTelefoneRepository representanteTelefoneRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _onboardingRepository = onboardingRepository;
            _consultorRepository = consultorRepository;
            _dadosBancariosRepository = dadosBancariosRepository;
            _enderecoRepository = enderecoRepository;
            _representanteRepository = representanteRepository;
            _representanteEmailRepository = representanteEmailRepository;
            _representanteTelefoneRepository = representanteTelefoneRepository;
        }

        public async Task<object> Handle(ObterEnderecoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            var onboarding = _onboardingRepository.Find(o => o.CodigoConvite.Equals(message.CodigoConvite) && o.NumeroCelular.Equals(message.NumeroCelular)).FirstOrDefault();
            if (onboarding == null)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Código do convite ou número de celular inválidos."));
                return await Task.FromResult(false);
            }

            if (onboarding.Consultor == null)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Consultor não cadastrado."));
                return await Task.FromResult(false);
            }

            var enderecos = _enderecoRepository.Find(e => e.Consultor.Id.Equals(onboarding.Consultor.Id)).OrderByDescending(e => e.Principal).ToList();

            return await Task.FromResult(enderecos);
        }

        public async Task<object> Handle(ObterEnderecoPorIdCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            var onboarding = _onboardingRepository.Find(o => o.CodigoConvite.Equals(message.CodigoConvite) && o.NumeroCelular.Equals(message.NumeroCelular)).FirstOrDefault();
            if (onboarding == null)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Código do convite ou número de celular inválidos."));
                return await Task.FromResult(false);
            }

            if (onboarding.Consultor == null)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Consultor não cadastrado."));
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

            var onboarding = _onboardingRepository.Find(o => o.CodigoConvite.Equals(message.CodigoConvite) && o.NumeroCelular.Equals(message.NumeroCelular)).FirstOrDefault();
            if (onboarding == null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Código do convite ou número de celular inválidos."));
                return Task.FromResult(false);
            }

            if (onboarding.Consultor == null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Consultor não cadastrado."));
                return Task.FromResult(false);
            }

            if (message.Principal)
                foreach (var item in onboarding.Consultor.Enderecos.Where(e => e.TipoEndereco.Equals((TipoEndereco)message.TipoEndereco)))
                    item.Principal = false;

            var endereco = new Endereco(Guid.NewGuid(), (TipoEndereco)message.TipoEndereco, message.Cep, message.Logradouro, message.Numero,
                message.Complemento, message.Bairro, message.Cidade, message.Estado, message.Principal,
                message.ComprovanteBase64, onboarding.Consultor.Id);

            _enderecoRepository.Add(endereco);

            onboarding.StatusOnboarding = ((TipoEndereco)message.TipoEndereco).Equals(TipoEndereco.Consultor) ? StatusOnboarding.EnderecoCnpj : StatusOnboarding.EnderecoRepresentante;

            _onboardingRepository.Update(onboarding);

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

            var onboarding = _onboardingRepository.Find(o => o.CodigoConvite.Equals(message.CodigoConvite) && o.NumeroCelular.Equals(message.NumeroCelular)).FirstOrDefault();
            if (onboarding == null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Código do convite ou número de celular inválidos."));
                return Task.FromResult(false);
            }

            if (onboarding.Consultor == null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Consultor não cadastrado."));
                return Task.FromResult(false);
            }

            var endereco = _enderecoRepository.GetById(message.EnderecoId);
            if (endereco != null)
            {
                _enderecoRepository.Remove(message.EnderecoId);

                if (Commit())
                {
                }

                return Task.FromResult(true);
            }

            _bus.RaiseEvent(new DomainNotification(message.MessageType, "Endereço não encontrado."));
            return Task.FromResult(false);
        }

        public Task<bool> Handle(CadastrarPessoaJuridicaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var onboarding = _onboardingRepository.Find(o => o.CodigoConvite.Equals(message.CodigoConvite) && o.NumeroCelular.Equals(message.NumeroCelular)).FirstOrDefault();
            if (onboarding == null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Código do convite ou número de celular inválidos."));
                return Task.FromResult(false);
            }

            if (onboarding.Consultor != null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "O consultor já possui cadastro no sistema."));
                return Task.FromResult(false);
            }

            var consultor = new Consultor(Guid.NewGuid(), message.Cnpj, message.RazaoSocial, message.NomeFantasia, message.InscricaoMunicipal,
                message.InscricaoEstadual, message.ContratoSocialBase64, message.UltimaAlteracaoBase64, onboarding.Id);

            _consultorRepository.Add(consultor);

            var dadosBancarios = new DadosBancarios(Guid.NewGuid(), message.CodigoBanco, message.Agencia, message.ContaSemDigito, message.Digito,
                message.TipoConta.ToString(), consultor.Id);

            _dadosBancariosRepository.Add(dadosBancarios);

            onboarding.StatusOnboarding = StatusOnboarding.ContratoSocial;

            _onboardingRepository.Update(onboarding);

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

            var onboarding = _onboardingRepository.Find(o => o.CodigoConvite.Equals(message.CodigoConvite) && o.NumeroCelular.Equals(message.NumeroCelular)).FirstOrDefault();
            if (onboarding == null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Código do convite ou número de celular inválidos."));
                return Task.FromResult(false);
            }

            if (onboarding.Consultor == null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Consultor não cadastrado."));
                return Task.FromResult(false);
            }

            var representante = new Representante(Guid.NewGuid(), message.Cpf, message.NomeCompleto, message.Sexo, message.EstadoCivil,
                message.Nacionalidade, message.DocumentoFrenteBase64, message.DocumentoVersoBase64, onboarding.Consultor.Id);

            _representanteRepository.Add(representante);

            foreach (var value in message.Emails)
            {
                var tipoEmail = (int?)value.GetType().GetProperty("TipoEmail")?.GetValue(value, null);
                var email = value.GetType().GetProperty("Email")?.GetValue(value, null).ToString();

                var representanteEmail = new RepresentanteEmail(Guid.NewGuid(), tipoEmail.GetValueOrDefault(), email, representante.Id);

                _representanteEmailRepository.Add(representanteEmail);
            }

            foreach (var value in message.Telefones)
            {
                var tipoTelefone = (int?)value.GetType().GetProperty("TipoTelefone")?.GetValue(value, null);
                var numero = value.GetType().GetProperty("Numero")?.GetValue(value, null).ToString();

                var representanteTelefone = new RepresentanteTelefone(Guid.NewGuid(), tipoTelefone.GetValueOrDefault(), numero, representante.Id);

                _representanteTelefoneRepository.Add(representanteTelefone);
            }

            onboarding.StatusOnboarding = StatusOnboarding.DadosRepresentante;

            _onboardingRepository.Update(onboarding);

            if (Commit())
            {
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

            var onboarding = _onboardingRepository.Find(o => o.CodigoConvite.Equals(message.CodigoConvite) && o.NumeroCelular.Equals(message.NumeroCelular)).FirstOrDefault();
            if (onboarding == null)
            {
                await _bus.RaiseEvent(new DomainNotification(message.MessageType, "Código do convite ou número de celular inválidos."));
                return await Task.FromResult(false);
            }

            return await Task.FromResult(onboarding);
        }
    }
}