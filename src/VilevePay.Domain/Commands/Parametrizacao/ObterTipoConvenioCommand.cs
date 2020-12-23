using MediatR;
using VilevePay.Domain.Validations.Parametrizacao;

namespace VilevePay.Domain.Commands.Parametrizacao
{
    public class ObterTipoConvenioCommand : ParametrizacaoCommand, IRequest<object>
    {
        public ObterTipoConvenioCommand()
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterTipoConvenioCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}