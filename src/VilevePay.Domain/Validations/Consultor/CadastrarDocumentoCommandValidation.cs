using VilevePay.Domain.Commands.Consultor;

namespace VilevePay.Domain.Validations.Consultor
{
    public class CadastrarDocumentoCommandValidation : ConsultorValidation<CadastrarDocumentoCommand>
    {
        public CadastrarDocumentoCommandValidation()
        {
            ValidateCodigoConvite();
        }
    }
}