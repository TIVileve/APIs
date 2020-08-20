using MediatR;
using VilevePay.Domain.Validations.Autorizacao;

namespace VilevePay.Domain.Commands.Autorizacao
{
    public class EnviarSmsTokenCommand : AutorizacaoCommand, IRequest<bool>
    {
        public EnviarSmsTokenCommand(string codigoConvite, string numeroCelular)
        {
            CodigoConvite = codigoConvite;
            NumeroCelular = numeroCelular;
        }

        public override bool IsValid()
        {
            ValidationResult = new EnviarSmsTokenCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}