using VilevePay.Domain.Commands.Consultor;

namespace VilevePay.Domain.Validations.Consultor
{
    public class ObterOnboardingStatusCommandValidation : ConsultorValidation<ObterOnboardingStatusCommand>
    {
        public ObterOnboardingStatusCommandValidation()
        {
            ValidateCodigoConvite();
        }
    }
}