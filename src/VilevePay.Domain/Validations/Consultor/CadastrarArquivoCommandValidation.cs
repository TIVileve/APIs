using VilevePay.Domain.Commands.Consultor;

namespace VilevePay.Domain.Validations.Consultor
{
    public class CadastrarArquivoCommandValidation : ConsultorValidation<CadastrarArquivoCommand>
    {
        public CadastrarArquivoCommandValidation()
        {
            ValidateCodigoConvite();
        }
    }
}