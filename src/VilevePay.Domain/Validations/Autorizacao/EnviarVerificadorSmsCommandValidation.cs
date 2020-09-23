using VilevePay.Domain.Commands.Autorizacao;

namespace VilevePay.Domain.Validations.Autorizacao
{
    public class EnviarVerificadorSmsCommandValidation : AutorizacaoValidation<EnviarVerificadorSmsCommand>
    {
        public EnviarVerificadorSmsCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateNumeroCelular();
        }
    }
}