using VilevePay.Domain.Commands.Cliente;

namespace VilevePay.Domain.Validations.Cliente
{
    public class RegistrarComprovantePessoaJuridicaCommandValidation : ClienteValidation<RegistrarComprovantePessoaJuridicaCommand>
    {
        public RegistrarComprovantePessoaJuridicaCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateComprovanteBase64();
        }
    }
}