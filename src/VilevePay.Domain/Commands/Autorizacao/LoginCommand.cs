using MediatR;
using VilevePay.Domain.Validations.Autorizacao;

namespace VilevePay.Domain.Commands.Autorizacao
{
    public class LoginCommand : AutorizacaoCommand, IRequest<object>
    {
        public LoginCommand(string usuario, string senha)
        {
            Usuario = usuario;
            Senha = senha;
        }

        public override bool IsValid()
        {
            ValidationResult = new LoginCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}