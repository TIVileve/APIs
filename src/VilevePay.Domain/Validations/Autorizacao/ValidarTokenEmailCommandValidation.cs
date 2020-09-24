using VilevePay.Domain.Commands.Autorizacao;

namespace VilevePay.Domain.Validations.Autorizacao
{
    public class ValidarTokenEmailCommandValidation : AutorizacaoValidation<ValidarTokenEmailCommand>
    {
        public ValidarTokenEmailCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateEmail();
            ValidateCodigoToken();
        }
    }
}