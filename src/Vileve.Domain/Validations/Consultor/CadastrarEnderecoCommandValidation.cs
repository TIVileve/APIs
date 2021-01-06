using Vileve.Domain.Commands.Consultor;

namespace Vileve.Domain.Validations.Consultor
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