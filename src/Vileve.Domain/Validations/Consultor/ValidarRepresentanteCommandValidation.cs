using Vileve.Domain.Commands.Consultor;

namespace Vileve.Domain.Validations.Consultor
{
    public class ValidarRepresentanteCommandValidation : ConsultorValidation<ValidarRepresentanteCommand>
    {
        public ValidarRepresentanteCommandValidation()
        {
            ValidateCpf();
        }
    }
}