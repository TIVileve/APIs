using VilevePay.Domain.Commands.Autorizacao;

namespace VilevePay.Domain.Validations.Autorizacao
{
    public class EnviarTokenEmailCommandValidation : AutorizacaoValidation<EnviarTokenEmailCommand>
    {
        public EnviarTokenEmailCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateEmail();
        }
    }
}