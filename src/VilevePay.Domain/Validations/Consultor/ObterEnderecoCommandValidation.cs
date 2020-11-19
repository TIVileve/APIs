using VilevePay.Domain.Commands.Consultor;

namespace VilevePay.Domain.Validations.Consultor
{
    public class ObterEnderecoCommandValidation : ConsultorValidation<ObterEnderecoCommand>
    {
        public ObterEnderecoCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateNumeroCelular();
        }
    }
}