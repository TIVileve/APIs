using Vileve.Domain.Commands.Consultor;

namespace Vileve.Domain.Validations.Consultor
{
    public class ValidarConsultorCommandValidation : ConsultorValidation<ValidarConsultorCommand>
    {
        public ValidarConsultorCommandValidation()
        {
            ValidateEmail();
        }
    }
}