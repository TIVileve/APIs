using MediatR;
using Vileve.Domain.Validations.Autorizacao;

namespace Vileve.Domain.Commands.Autorizacao
{
    public class EnviarTokenEmailCommand : AutorizacaoCommand, IRequest<bool>
    {
        public EnviarTokenEmailCommand(string email)
        {
            Email = email;
        }

        public override bool IsValid()
        {
            ValidationResult = new EnviarTokenEmailCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}