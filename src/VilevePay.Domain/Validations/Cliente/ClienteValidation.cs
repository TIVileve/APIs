using FluentValidation;
using VilevePay.Domain.Commands.Cliente;

namespace VilevePay.Domain.Validations.Cliente
{
    public abstract class ClienteValidation<T> : AbstractValidator<T> where T : ClienteCommand
    {
        protected void ValidateCodigoConvite()
        {
            RuleFor(c => c.CodigoConvite)
                .NotEmpty().WithMessage("O campo código do convite é obrigatório.")
                .Length(4).WithMessage("O campo código do convite deve ter 4 caracteres.");
        }

        protected void ValidateCpf()
        {
            RuleFor(c => c.Cpf)
                .NotEmpty().WithMessage("O campo CPF é obrigatório.")
                .IsValidCPF().WithMessage("O campo CPF está inválido.");
        }

        protected void ValidateCnpj()
        {
            RuleFor(c => c.Cnpj)
                .NotEmpty().WithMessage("O campo CNPJ é obrigatório.")
                .IsValidCNPJ().WithMessage("O campo CNPJ está inválido.");
        }

        protected void ValidateComprovanteBase64()
        {
            RuleFor(c => c.ComprovanteBase64)
                .NotEmpty().WithMessage("O campo comprovante base64 é obrigatório.");
        }

        protected void ValidateCep()
        {
            RuleFor(c => c.Cep)
                .NotEmpty().WithMessage("O campo CEP é obrigatório.")
                .Length(2, 100).WithMessage("O campo CEP deve ter entre 2 e 100 caracteres.");
        }

        protected void ValidateLogradouro()
        {
            RuleFor(c => c.Logradouro)
                .NotEmpty().WithMessage("O campo logradouro é obrigatório.")
                .Length(2, 100).WithMessage("O campo logradouro deve ter entre 2 e 100 caracteres.");
        }

        protected void ValidateNumero()
        {
            RuleFor(c => c.Numero)
                .NotNull().WithMessage("O campo número é obrigatório.")
                .GreaterThan(0).WithMessage("O campo número deve ser maior que zero.");
        }

        protected void ValidateComplemento()
        {
            RuleFor(c => c.Complemento)
                .Length(2, 100).WithMessage("O campo complemento deve ter entre 2 e 100 caracteres.")
                .When(e => !string.IsNullOrEmpty(e.Complemento));
        }

        protected void ValidateBairro()
        {
            RuleFor(c => c.Bairro)
                .NotEmpty().WithMessage("O campo bairro é obrigatório.")
                .Length(2, 100).WithMessage("O campo bairro deve ter entre 2 e 100 caracteres.");
        }

        protected void ValidateCidade()
        {
            RuleFor(c => c.Cidade)
                .NotEmpty().WithMessage("O campo cidade é obrigatório.")
                .Length(2, 100).WithMessage("O campo cidade deve ter entre 2 e 100 caracteres.");
        }

        protected void ValidateEstado()
        {
            RuleFor(c => c.Estado)
                .NotEmpty().WithMessage("O campo estado é obrigatório.")
                .Length(2, 100).WithMessage("O campo estado deve ter entre 2 e 100 caracteres.");
        }

        protected void ValidateComprovanteEnderecoBase64()
        {
            RuleFor(c => c.ComprovanteEnderecoBase64)
                .NotEmpty().WithMessage("O campo comprovante de endereço base64 é obrigatório.");
        }
    }
}