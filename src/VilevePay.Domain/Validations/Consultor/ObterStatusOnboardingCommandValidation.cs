using VilevePay.Domain.Commands.Consultor;

namespace VilevePay.Domain.Validations.Consultor
{
    public class ObterStatusOnboardingCommandValidation : ConsultorValidation<ObterStatusOnboardingCommand>
    {
        public ObterStatusOnboardingCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateNumeroCelular();
        }
    }
}