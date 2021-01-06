using Vileve.Domain.Commands.Autorizacao;

namespace Vileve.Domain.Validations.Autorizacao
{
    public class EnviarTokenSmsCommandValidation : AutorizacaoValidation<EnviarTokenSmsCommand>
    {
        public EnviarTokenSmsCommandValidation()
        {
            ValidateNumeroCelular();
        }
    }
}