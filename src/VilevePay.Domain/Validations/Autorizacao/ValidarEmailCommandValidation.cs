using VilevePay.Domain.Commands.Autorizacao;

namespace VilevePay.Domain.Validations.Autorizacao
{
    public class ValidarEmailCommandValidation : AutorizacaoValidation<ValidarEmailCommand>
    {
        public ValidarEmailCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateEmail();
        }
    }
}