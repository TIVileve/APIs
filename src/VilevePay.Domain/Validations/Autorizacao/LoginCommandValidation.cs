using VilevePay.Domain.Commands.Autorizacao;

namespace VilevePay.Domain.Validations.Autorizacao
{
    public class LoginCommandValidation : AutorizacaoValidation<LoginCommand>
    {
        public LoginCommandValidation()
        {
            ValidateUsuario();
            ValidateSenha();
        }
    }
}