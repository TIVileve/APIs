using MediatR;
using VilevePay.Domain.Validations.Autorizacao;

namespace VilevePay.Domain.Commands.Autorizacao
{
    public class EnviarVerificadorEmailCommand : AutorizacaoCommand, IRequest<bool>
    {
        public EnviarVerificadorEmailCommand(string codigoConvite, string email)
        {
            CodigoConvite = codigoConvite;
            Email = email;
        }

        public override bool IsValid()
        {
            ValidationResult = new EnviarVerificadorEmailCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}