using Vileve.Domain.Commands.Consultor;

namespace Vileve.Domain.Validations.Consultor
{
    public class ValidarPessoaJuridicaCommandValidation : ConsultorValidation<ValidarPessoaJuridicaCommand>
    {
        public ValidarPessoaJuridicaCommandValidation()
        {
            ValidateCnpj();
        }
    }
}