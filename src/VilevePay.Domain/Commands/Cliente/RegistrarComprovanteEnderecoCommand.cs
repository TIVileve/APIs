using MediatR;
using VilevePay.Domain.Validations.Cliente;

namespace VilevePay.Domain.Commands.Cliente
{
    public class RegistrarComprovanteEnderecoCommand : ClienteCommand, IRequest<bool>
    {
        public RegistrarComprovanteEnderecoCommand(string codigoConvite, string comprovanteEnderecoBase64)
        {
            CodigoConvite = codigoConvite;
            ComprovanteEnderecoBase64 = comprovanteEnderecoBase64;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegistrarComprovanteEnderecoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}