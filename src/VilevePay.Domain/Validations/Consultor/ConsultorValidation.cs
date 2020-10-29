using System;
using FluentValidation;
using VilevePay.Domain.Commands.Consultor;

namespace VilevePay.Domain.Validations.Consultor
{
    public abstract class ConsultorValidation<T> : AbstractValidator<T> where T : ConsultorCommand
    {
        protected void ValidateCodigoConvite()
        {
            RuleFor(c => c.CodigoConvite)
                .NotEmpty().WithMessage("O campo código do convite é obrigatório.")
                .Length(6).WithMessage("O campo código do convite deve ter 6 caracteres.");
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
    }
}