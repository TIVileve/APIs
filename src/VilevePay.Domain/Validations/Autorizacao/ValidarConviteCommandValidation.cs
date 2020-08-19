using VilevePay.Domain.Commands.Autorizacao;

namespace VilevePay.Domain.Validations.Autorizacao
{
    public class ValidarConviteCommandValidation : AutorizacaoValidation<ValidarConviteCommand>
    {
        public ValidarConviteCommandValidation()
        {
            ValidateCodigoConvite();
        }
    }
}