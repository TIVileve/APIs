using Vileve.Domain.Commands.Consultor;

namespace Vileve.Domain.Validations.Consultor
{
    public class CadastrarPessoaJuridicaCommandValidation : ConsultorValidation<CadastrarPessoaJuridicaCommand>
    {
        public CadastrarPessoaJuridicaCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateNumeroCelular();
        }
    }
}