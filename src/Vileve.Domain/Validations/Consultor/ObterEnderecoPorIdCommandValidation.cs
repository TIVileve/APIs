using Vileve.Domain.Commands.Consultor;

namespace Vileve.Domain.Validations.Consultor
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