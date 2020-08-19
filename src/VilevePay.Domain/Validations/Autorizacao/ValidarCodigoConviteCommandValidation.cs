using VilevePay.Domain.Commands.Autorizacao;

namespace VilevePay.Domain.Validations.Autorizacao
{
    public class ValidarCodigoConviteCommandValidation : AutorizacaoValidation<ValidarCodigoConviteCommand>
    {
        public ValidarCodigoConviteCommandValidation()
        {
            ValidateCodigoConvite();
        }
    }
}