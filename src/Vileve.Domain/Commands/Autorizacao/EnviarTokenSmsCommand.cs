using MediatR;
using Vileve.Domain.Validations.Autorizacao;

namespace Vileve.Domain.Commands.Autorizacao
{
    public class EnviarTokenSmsCommand : AutorizacaoCommand, IRequest<bool>
    {
        public EnviarTokenSmsCommand(string numeroCelular)
        {
            NumeroCelular = numeroCelular;
        }

        public override bool IsValid()
        {
            ValidationResult = new EnviarTokenSmsCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}