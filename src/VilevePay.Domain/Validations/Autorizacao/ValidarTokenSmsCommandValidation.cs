using VilevePay.Domain.Commands.Autorizacao;

namespace VilevePay.Domain.Validations.Autorizacao
{
    public class ValidarTokenSmsCommandValidation : AutorizacaoValidation<ValidarTokenSmsCommand>
    {
        public ValidarTokenSmsCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateNumeroCelular();
            ValidateCodigoToken();
        }
    }
}