using MediatR;
using VilevePay.Domain.Validations.Cliente;

namespace VilevePay.Domain.Commands.Cliente
{
    public class RegistrarComprovantePessoaFisicaCommand : ClienteCommand, IRequest<bool>
    {
        public RegistrarComprovantePessoaFisicaCommand(string codigoConvite, string comprovanteBase64)
        {
            CodigoConvite = codigoConvite;
            ComprovanteBase64 = comprovanteBase64;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegistrarComprovantePessoaFisicaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}