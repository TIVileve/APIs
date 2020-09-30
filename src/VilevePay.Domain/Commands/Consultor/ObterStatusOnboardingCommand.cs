using MediatR;
using VilevePay.Domain.Validations.Consultor;

namespace VilevePay.Domain.Commands.Consultor
{
    public class ObterStatusOnboardingCommand : ConsultorCommand, IRequest<object>
    {
        public ObterStatusOnboardingCommand(string codigoConvite)
        {
            CodigoConvite = codigoConvite;
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterStatusOnboardingCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}