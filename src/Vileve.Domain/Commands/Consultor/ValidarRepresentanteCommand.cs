using MediatR;
using Vileve.Domain.Validations.Consultor;

namespace Vileve.Domain.Commands.Consultor
{
    public class ValidarRepresentanteCommand : ConsultorCommand, IRequest<bool>
    {
        public ValidarRepresentanteCommand(string cpf)
        {
            Cpf = cpf;
        }

        public override bool IsValid()
        {
            ValidationResult = new ValidarRepresentanteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}