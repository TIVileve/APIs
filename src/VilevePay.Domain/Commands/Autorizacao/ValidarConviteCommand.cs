using MediatR;
using VilevePay.Domain.Validations.Autorizacao;

namespace VilevePay.Domain.Commands.Autorizacao
{
    public class ValidarConviteCommand : AutorizacaoCommand, IRequest<bool>
    {
        public ValidarConviteCommand(string codigoConvite)
        {
            CodigoConvite = codigoConvite;
        }

        public override bool IsValid()
        {
            ValidationResult = new ValidarConviteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}