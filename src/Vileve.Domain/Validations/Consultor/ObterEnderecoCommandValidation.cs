using Vileve.Domain.Commands.Consultor;

namespace Vileve.Domain.Validations.Consultor
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