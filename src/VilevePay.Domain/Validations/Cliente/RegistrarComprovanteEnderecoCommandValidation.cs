using VilevePay.Domain.Commands.Cliente;

namespace VilevePay.Domain.Validations.Cliente
{
    public class RegistrarComprovanteEnderecoCommandValidation : ClienteValidation<RegistrarComprovanteEnderecoCommand>
    {
        public RegistrarComprovanteEnderecoCommandValidation()
        {
            ValidateCodigoConvite();
            ValidateComprovanteEnderecoBase64();
        }
    }
}