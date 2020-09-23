using VilevePay.Domain.Commands.Autorizacao;

namespace VilevePay.Domain.Validations.Autorizacao
{
    public class ValidarSmsCommandValidation : AutorizacaoValidation<ValidarSmsCommand>
    {
        public ValidarSmsCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateNumeroCelular();
            ValidateCodigoToken();
        }
    }
}