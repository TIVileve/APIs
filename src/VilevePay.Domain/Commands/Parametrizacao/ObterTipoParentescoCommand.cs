using MediatR;
using VilevePay.Domain.Validations.Parametrizacao;

namespace VilevePay.Domain.Commands.Parametrizacao
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