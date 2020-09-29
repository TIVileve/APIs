using VilevePay.Domain.Commands.Consultor;

namespace VilevePay.Domain.Validations.Consultor
{
    public class CadastrarEmailCommandValidation : ConsultorValidation<CadastrarEmailCommand>
    {
        public CadastrarEmailCommandValidation()
        {
            ValidateCodigoConvite();
        }
    }
}