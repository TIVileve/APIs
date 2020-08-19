using MediatR;
using VilevePay.Domain.Validations.Autorizacao;

namespace VilevePay.Domain.Commands.Autorizacao
{
    public class ValidarCodigoTokenCommand : AutorizacaoCommand, IRequest<bool>
    {
        public ValidarCodigoTokenCommand(string codigoConvite, string codigoToken)
        {
            CodigoConvite = codigoConvite;
            CodigoToken = codigoToken;
        }

        public override bool IsValid()
        {
            ValidationResult = new ValidarCodigoTokenCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}