using Vileve.Domain.Commands.Autorizacao;

namespace Vileve.Domain.Validations.Autorizacao
{
    public class EnviarTokenEmailCommandValidation : AutorizacaoValidation<EnviarTokenEmailCommand>
    {
        public EnviarTokenEmailCommandValidation()
        {
            ValidateEmail();
        }
    }
}