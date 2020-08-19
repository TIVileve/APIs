using MediatR;
using VilevePay.Domain.Validations.Autorizacao;

namespace VilevePay.Domain.Commands.Autorizacao
{
    public class ValidarCodigoConviteCommand : AutorizacaoCommand, IRequest<bool>
    {
        public ValidarCodigoConviteCommand(string codigoConvite)
        {
            CodigoConvite = codigoConvite;
        }

        public override bool IsValid()
        {
            ValidationResult = new ValidarCodigoConviteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}