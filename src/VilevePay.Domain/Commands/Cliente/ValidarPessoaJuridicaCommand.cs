using MediatR;
using VilevePay.Domain.Validations.Cliente;

namespace VilevePay.Domain.Commands.Cliente
{
    public class ValidarPessoaJuridicaCommand : ClienteCommand, IRequest<bool>
    {
        public ValidarPessoaJuridicaCommand(string codigoConvite, string cnpj)
        {
            CodigoConvite = codigoConvite;
            Cnpj = cnpj;
        }

        public override bool IsValid()
        {
            ValidationResult = new ValidarPessoaJuridicaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}