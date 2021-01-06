using MediatR;
using Vileve.Domain.Validations.Autorizacao;

namespace Vileve.Domain.Commands.Autorizacao
{
    public class ValidarTokenSmsCommand : AutorizacaoCommand, IRequest<bool>
    {
        public ValidarTokenSmsCommand(string codigoConvite, string numeroCelular, string codigoToken)
        {
            CodigoConvite = codigoConvite;
            NumeroCelular = numeroCelular;
            CodigoToken = codigoToken;
        }

        public override bool IsValid()
        {
            ValidationResult = new ValidarTokenSmsCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}