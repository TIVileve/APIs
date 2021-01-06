using Vileve.Domain.Commands.Autorizacao;

namespace Vileve.Domain.Validations.Autorizacao
{
    public class ValidarCodigoConviteCommandValidation : AutorizacaoValidation<ValidarCodigoConviteCommand>
    {
        public ValidarCodigoConviteCommandValidation()
        {
            ValidateCodigoConvite();
        }
    }
}