using System;
using FluentValidation;
using Vileve.Domain.Commands.Consultor;

namespace Vileve.Domain.Validations.Consultor
{
    public abstract class ConsultorValidation<T> : AbstractValidator<T> where T : ConsultorCommand
    {
        protected void ValidateCodigoConvite()
        {
            RuleFor(c => c.CodigoConvite)
                .NotEmpty().WithMessage("O campo código do convite é obrigatório.")
                .Length(6).WithMessage("O campo código do convite deve ter 6 caracteres.");
        }

        protected void ValidateNumeroCelular()
        {
            RuleFor(c => c.NumeroCelular)
                .NotEmpty().WithMessage("O campo número de celular é obrigatório.")
                .Length(13, 14).WithMessage("O campo número de celular deve ter entre 13 e 14 caracteres.");
        }

        protected void ValidateEnderecoId()
        {
            RuleFor(c => c.EnderecoId)
                .NotEqual(Guid.Empty).WithMessage("O campo endereço id é obrigatório.");
        }

        protected void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O campo e-mail é obrigatório.")
                .EmailAddress().WithMessage("O campo e-mail está inválido.");
        }

        protected void ValidateCnpj()
        {
            RuleFor(c => c.Cnpj)
                .NotEmpty().WithMessage("O campo CNPJ é obrigatório.")
                .IsValidCNPJ().WithMessage("O campo CNPJ está inválido.");
        }

        protected void ValidateCpf()
        {
            RuleFor(c => c.Cpf)
                .NotEmpty().WithMessage("O campo CPF é obrigatório.")
                .IsValidCPF().WithMessage("O campo CPF está inválido.");
        }
    }
}