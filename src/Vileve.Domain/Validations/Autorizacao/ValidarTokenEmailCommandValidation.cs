using Vileve.Domain.Commands.Autorizacao;

namespace Vileve.Domain.Validations.Autorizacao
{
    public class ValidarTokenEmailCommandValidation : AutorizacaoValidation<ValidarTokenEmailCommand>
    {
        public ValidarTokenEmailCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateNumeroCelular();
            ValidateEmail();
            ValidateCodigoToken();
        }
    }
}