using VilevePay.Domain.Commands.Consultor;

namespace VilevePay.Domain.Validations.Consultor
{
    public class ObterEnderecoPorIdCommandValidation : ConsultorValidation<ObterEnderecoPorIdCommand>
    {
        public ObterEnderecoPorIdCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateNumeroCelular();
            ValidateEnderecoId();
        }
    }
}