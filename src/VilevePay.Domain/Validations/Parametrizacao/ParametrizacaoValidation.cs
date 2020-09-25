using FluentValidation;
using VilevePay.Domain.Commands.Parametrizacao;

namespace VilevePay.Domain.Validations.Parametrizacao
{
    public abstract class ParametrizacaoValidation<T> : AbstractValidator<T> where T : ParametrizacaoCommand
    {
    }
}