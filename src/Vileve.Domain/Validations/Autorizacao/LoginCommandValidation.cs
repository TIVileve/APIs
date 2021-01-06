using Vileve.Domain.Commands.Autorizacao;

namespace Vileve.Domain.Validations.Autorizacao
{
    public class LoginCommandValidation : AutorizacaoValidation<LoginCommand>
    {
        public LoginCommandValidation()
        {
            ValidateEmail();
            ValidateSenha();
        }
    }
}