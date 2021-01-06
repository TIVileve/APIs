using MediatR;
using Vileve.Domain.Validations.Parametrizacao;

namespace Vileve.Domain.Commands.Parametrizacao
{
    public class ObterTipoParentescoCommand : ParametrizacaoCommand, IRequest<object>
    {
        public ObterTipoParentescoCommand()
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterTipoParentescoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}