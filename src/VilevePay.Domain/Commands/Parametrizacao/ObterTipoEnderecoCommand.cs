using MediatR;
using VilevePay.Domain.Validations.Parametrizacao;

namespace VilevePay.Domain.Commands.Parametrizacao
{
    public class ObterTipoEnderecoCommand : ParametrizacaoCommand, IRequest<object>
    {
        public ObterTipoEnderecoCommand()
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterTipoEnderecoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}