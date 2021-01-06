using MediatR;
using Vileve.Domain.Validations.Parametrizacao;

namespace Vileve.Domain.Commands.Parametrizacao
{
    public class ObterEstadoCivilCommand : ParametrizacaoCommand, IRequest<object>
    {
        public ObterEstadoCivilCommand()
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterEstadoCivilCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}