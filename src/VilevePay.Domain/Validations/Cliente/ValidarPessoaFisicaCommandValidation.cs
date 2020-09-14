using VilevePay.Domain.Commands.Cliente;

namespace VilevePay.Domain.Validations.Cliente
{
    public class ValidarPessoaFisicaCommandValidation : ClienteValidation<ValidarPessoaFisicaCommand>
    {
        public ValidarPessoaFisicaCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateCpf();
        }
    }
}