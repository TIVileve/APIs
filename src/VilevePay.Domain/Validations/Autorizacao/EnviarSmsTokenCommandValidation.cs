using VilevePay.Domain.Commands.Autorizacao;

namespace VilevePay.Domain.Validations.Autorizacao
{
    public class EnviarSmsTokenCommandValidation : AutorizacaoValidation<EnviarSmsTokenCommand>
    {
        public EnviarSmsTokenCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateNumeroCelular();
        }
    }
}