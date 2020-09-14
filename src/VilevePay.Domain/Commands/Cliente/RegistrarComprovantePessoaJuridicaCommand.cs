using MediatR;
using VilevePay.Domain.Validations.Cliente;

namespace VilevePay.Domain.Commands.Cliente
{
    public class RegistrarComprovantePessoaJuridicaCommand : ClienteCommand, IRequest<bool>
    {
        public RegistrarComprovantePessoaJuridicaCommand(string codigoConvite, string comprovanteBase64)
        {
            CodigoConvite = codigoConvite;
            ComprovanteBase64 = comprovanteBase64;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegistrarComprovantePessoaJuridicaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}