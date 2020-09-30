using MediatR;
using VilevePay.Domain.Validations.Consultor;

namespace VilevePay.Domain.Commands.Consultor
{
    public class ObterOnboardingStatusCommand : ConsultorCommand, IRequest<object>
    {
        public ObterOnboardingStatusCommand(string codigoConvite)
        {
            CodigoConvite = codigoConvite;
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterOnboardingStatusCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}