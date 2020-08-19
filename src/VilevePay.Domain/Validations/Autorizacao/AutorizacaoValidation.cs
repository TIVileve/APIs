using FluentValidation;
using VilevePay.Domain.Commands.Autorizacao;

namespace VilevePay.Domain.Validations.Autorizacao
{
    public abstract class AutorizacaoValidation<T> : AbstractValidator<T> where T : AutorizacaoCommand
    {
        protected void ValidateCodigoConvite()
        {
            RuleFor(a => a.CodigoConvite)
                .NotEmpty().WithMessage("O campo código do convite é obrigatório.")
                .Length(4).WithMessage("O campo código do convite deve ter 4 caracteres.");
        }
    }
}