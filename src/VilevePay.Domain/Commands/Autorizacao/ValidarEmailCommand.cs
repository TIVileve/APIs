using MediatR;
using VilevePay.Domain.Validations.Autorizacao;

namespace VilevePay.Domain.Commands.Autorizacao
{
    public class ValidarEmailCommand : AutorizacaoCommand, IRequest<bool>
    {
        public ValidarEmailCommand(string codigoConvite, string email)
        {
            CodigoConvite = codigoConvite;
            Email = email;
        }

        public override bool IsValid()
        {
            ValidationResult = new ValidarEmailCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}