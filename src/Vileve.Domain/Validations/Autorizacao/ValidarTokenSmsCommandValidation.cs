using Vileve.Domain.Commands.Autorizacao;

namespace Vileve.Domain.Validations.Autorizacao
{
    public class ValidarTokenSmsCommandValidation : AutorizacaoValidation<ValidarTokenSmsCommand>
    {
        public ValidarTokenSmsCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateNumeroCelular();
            ValidateCodigoToken();
        }
    }
}