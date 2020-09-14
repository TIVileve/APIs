using MediatR;
using VilevePay.Domain.Validations.Cliente;

namespace VilevePay.Domain.Commands.Cliente
{
    public class ValidarPessoaFisicaCommand : ClienteCommand, IRequest<bool>
    {
        public ValidarPessoaFisicaCommand(string codigoConvite, string cpf)
        {
            CodigoConvite = codigoConvite;
            Cpf = cpf;
        }

        public override bool IsValid()
        {
            ValidationResult = new ValidarPessoaFisicaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}