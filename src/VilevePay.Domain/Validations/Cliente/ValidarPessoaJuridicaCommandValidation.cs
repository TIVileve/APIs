using VilevePay.Domain.Commands.Cliente;

namespace VilevePay.Domain.Validations.Cliente
{
    public class ValidarPessoaJuridicaCommandValidation : ClienteValidation<ValidarPessoaJuridicaCommand>
    {
        public ValidarPessoaJuridicaCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateCnpj();
        }
    }
}