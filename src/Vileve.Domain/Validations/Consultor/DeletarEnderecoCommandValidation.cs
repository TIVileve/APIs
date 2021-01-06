using Vileve.Domain.Commands.Consultor;

namespace Vileve.Domain.Validations.Consultor
{
    public class DeletarEnderecoCommandValidation : ConsultorValidation<DeletarEnderecoCommand>
    {
        public DeletarEnderecoCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateNumeroCelular();
            ValidateEnderecoId();
        }
    }
}