using VilevePay.Domain.Commands.Consultor;

namespace VilevePay.Domain.Validations.Consultor
{
    public class CadastrarPessoaJuridicaCommandValidation : ConsultorValidation<CadastrarPessoaJuridicaCommand>
    {
        public CadastrarPessoaJuridicaCommandValidation()
        {
            ValidateCodigoConvite();
        }
    }
}