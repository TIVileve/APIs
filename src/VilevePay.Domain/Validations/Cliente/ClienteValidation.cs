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
    }
}