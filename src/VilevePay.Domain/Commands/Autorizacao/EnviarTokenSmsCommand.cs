using MediatR;
using VilevePay.Domain.Validations.Autorizacao;

namespace VilevePay.Domain.Commands.Autorizacao
{
    public class EnviarTokenSmsCommand : AutorizacaoCommand, IRequest<bool>
    {
        public EnviarTokenSmsCommand(string codigoConvite, string numeroCelular)
        {
            CodigoConvite = codigoConvite;
            NumeroCelular = numeroCelular;
        }

        public override bool IsValid()
        {
            ValidationResult = new EnviarTokenSmsCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}