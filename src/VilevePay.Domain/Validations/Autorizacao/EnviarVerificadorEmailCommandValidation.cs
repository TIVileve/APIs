using VilevePay.Domain.Commands.Autorizacao;

namespace VilevePay.Domain.Validations.Autorizacao
{
    public class EnviarVerificadorEmailCommandValidation : AutorizacaoValidation<EnviarVerificadorEmailCommand>
    {
        public EnviarVerificadorEmailCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateEmail();
        }
    }
}