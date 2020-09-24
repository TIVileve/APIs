using VilevePay.Domain.Commands.Autorizacao;

namespace VilevePay.Domain.Validations.Autorizacao
{
    public class EnviarTokenSmsCommandValidation : AutorizacaoValidation<EnviarTokenSmsCommand>
    {
        public EnviarTokenSmsCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateNumeroCelular();
        }
    }
}