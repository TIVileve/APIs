using FluentValidation;
using Vileve.Domain.Commands.Parametrizacao;

namespace Vileve.Domain.Validations.Parametrizacao
{
    public abstract class ParametrizacaoValidation<T> : AbstractValidator<T> where T : ParametrizacaoCommand
    {
    }
}