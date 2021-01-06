using MediatR;
using Vileve.Domain.Validations.Autorizacao;

namespace Vileve.Domain.Commands.Autorizacao
{
    public class LoginCommand : AutorizacaoCommand, IRequest<object>
    {
        public LoginCommand(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        public override bool IsValid()
        {
            ValidationResult = new LoginCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}