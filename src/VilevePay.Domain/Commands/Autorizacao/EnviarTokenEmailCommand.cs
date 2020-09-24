using MediatR;
using VilevePay.Domain.Validations.Autorizacao;

namespace VilevePay.Domain.Commands.Autorizacao
{
    public class EnviarTokenEmailCommand : AutorizacaoCommand, IRequest<bool>
    {
        public EnviarTokenEmailCommand(string codigoConvite, string email)
        {
            CodigoConvite = codigoConvite;
            Email = email;
        }

        public override bool IsValid()
        {
            ValidationResult = new EnviarTokenEmailCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}