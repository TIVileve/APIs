using MediatR;
using VilevePay.Domain.Validations.Autorizacao;

namespace VilevePay.Domain.Commands.Autorizacao
{
    public class EnviarVerificadorSmsCommand : AutorizacaoCommand, IRequest<bool>
    {
        public EnviarVerificadorSmsCommand(string codigoConvite, string numeroCelular)
        {
            CodigoConvite = codigoConvite;
            NumeroCelular = numeroCelular;
        }

        public override bool IsValid()
        {
            ValidationResult = new EnviarVerificadorSmsCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}