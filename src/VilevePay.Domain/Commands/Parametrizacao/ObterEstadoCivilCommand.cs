using MediatR;
using VilevePay.Domain.Validations.Parametrizacao;

namespace VilevePay.Domain.Commands.Parametrizacao
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