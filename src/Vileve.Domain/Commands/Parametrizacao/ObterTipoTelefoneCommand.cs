using MediatR;
using Vileve.Domain.Validations.Parametrizacao;

namespace Vileve.Domain.Commands.Parametrizacao
{
    public class ObterTipoTelefoneCommand : ParametrizacaoCommand, IRequest<object>
    {
        public ObterTipoTelefoneCommand()
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterTipoTelefoneCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}