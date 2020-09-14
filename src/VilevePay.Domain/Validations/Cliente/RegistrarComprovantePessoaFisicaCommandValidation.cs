using VilevePay.Domain.Commands.Cliente;

namespace VilevePay.Domain.Validations.Cliente
{
    public class RegistrarComprovantePessoaFisicaCommandValidation : ClienteValidation<RegistrarComprovantePessoaFisicaCommand>
    {
        public RegistrarComprovantePessoaFisicaCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateComprovanteBase64();
        }
    }
}