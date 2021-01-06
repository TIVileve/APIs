using Vileve.Domain.Commands.Consultor;

namespace Vileve.Domain.Validations.Consultor
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