using MediatR;
using Vileve.Domain.Validations.Parametrizacao;

namespace Vileve.Domain.Commands.Parametrizacao
{
    public class ObterNacionalidadeCommand : ParametrizacaoCommand, IRequest<object>
    {
        public ObterNacionalidadeCommand()
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterNacionalidadeCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}