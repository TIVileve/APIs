using MediatR;
using Vileve.Domain.Validations.Parametrizacao;

namespace Vileve.Domain.Commands.Parametrizacao
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