using VilevePay.Domain.Commands.Consultor;

namespace VilevePay.Domain.Validations.Consultor
{
    public class CadastrarTelefoneCommandValidation : ConsultorValidation<CadastrarTelefoneCommand>
    {
        public CadastrarTelefoneCommandValidation()
        {
            ValidateCodigoConvite();
        }
    }
}