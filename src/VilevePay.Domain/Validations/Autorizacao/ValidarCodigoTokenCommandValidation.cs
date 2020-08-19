using VilevePay.Domain.Commands.Autorizacao;

namespace VilevePay.Domain.Validations.Autorizacao
{
    public class ValidarCodigoTokenCommandValidation : AutorizacaoValidation<ValidarCodigoTokenCommand>
    {
        public ValidarCodigoTokenCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateNumeroCelular();
            ValidateCodigoToken();
        }
    }
}