using VilevePay.Domain.Commands.Autorizacao;

namespace VilevePay.Domain.Validations.Autorizacao
{
    public class ValidarSelfieCommandValidation : AutorizacaoValidation<ValidarSelfieCommand>
    {
        public ValidarSelfieCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateFotoBase64();
        }
    }
}