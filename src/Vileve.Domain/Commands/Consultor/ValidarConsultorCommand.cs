using MediatR;
using Vileve.Domain.Validations.Consultor;

namespace Vileve.Domain.Commands.Consultor
{
    public class ValidarConsultorCommand : ConsultorCommand, IRequest<bool>
    {
        public ValidarConsultorCommand(string email)
        {
            Email = email;
        }

        public override bool IsValid()
        {
            ValidationResult = new ValidarConsultorCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}