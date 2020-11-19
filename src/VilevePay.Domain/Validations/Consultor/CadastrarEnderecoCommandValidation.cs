using VilevePay.Domain.Commands.Consultor;

namespace VilevePay.Domain.Validations.Consultor
{
    public class CadastrarEnderecoCommandValidation : ConsultorValidation<CadastrarEnderecoCommand>
    {
        public CadastrarEnderecoCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateNumeroCelular();
        }
    }
}