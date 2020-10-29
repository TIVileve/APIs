using VilevePay.Domain.Commands.Consultor;

namespace VilevePay.Domain.Validations.Consultor
{
    public class CadastrarRepresentanteCommandValidation : ConsultorValidation<CadastrarRepresentanteCommand>
    {
        public CadastrarRepresentanteCommandValidation()
        {
            ValidateCodigoConvite();
        }
    }
}