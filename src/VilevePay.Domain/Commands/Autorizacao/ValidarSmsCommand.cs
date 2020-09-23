using MediatR;
using VilevePay.Domain.Validations.Autorizacao;

namespace VilevePay.Domain.Commands.Autorizacao
{
    public class ValidarSmsCommand : AutorizacaoCommand, IRequest<bool>
    {
        public ValidarSmsCommand(string codigoConvite, string numeroCelular, string codigoToken)
        {
            CodigoConvite = codigoConvite;
            NumeroCelular = numeroCelular;
            CodigoToken = codigoToken;
        }

        public override bool IsValid()
        {
            ValidationResult = new ValidarSmsCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}