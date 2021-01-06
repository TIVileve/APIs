using MediatR;
using Vileve.Domain.Validations.Autorizacao;

namespace Vileve.Domain.Commands.Autorizacao
{
    public class ValidarTokenEmailCommand : AutorizacaoCommand, IRequest<bool>
    {
        public ValidarTokenEmailCommand(string codigoConvite, string numeroCelular, string email, string codigoToken)
        {
            CodigoConvite = codigoConvite;
            NumeroCelular = numeroCelular;
            Email = email;
            CodigoToken = codigoToken;
        }

        public override bool IsValid()
        {
            ValidationResult = new ValidarTokenEmailCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}