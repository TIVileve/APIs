using VilevePay.Domain.Commands.Consultor;

namespace VilevePay.Domain.Validations.Consultor
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