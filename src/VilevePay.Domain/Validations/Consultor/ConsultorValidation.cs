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
    }
}