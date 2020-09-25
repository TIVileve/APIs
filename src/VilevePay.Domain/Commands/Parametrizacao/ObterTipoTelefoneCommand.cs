using MediatR;
using VilevePay.Domain.Validations.Parametrizacao;

namespace VilevePay.Domain.Commands.Parametrizacao
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