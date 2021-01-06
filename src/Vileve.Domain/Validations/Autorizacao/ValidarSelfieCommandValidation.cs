using Vileve.Domain.Commands.Autorizacao;

namespace Vileve.Domain.Validations.Autorizacao
{
    public class ValidarSelfieCommandValidation : AutorizacaoValidation<ValidarSelfieCommand>
    {
        public ValidarSelfieCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateNumeroCelular();
            ValidateFotoBase64();
        }
    }
}