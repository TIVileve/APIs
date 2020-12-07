using MediatR;
using VilevePay.Domain.Validations.Cliente;

namespace VilevePay.Domain.Commands.Cliente
{
    public class CadastrarProdutoCommand : ClienteCommand, IRequest<bool>
    {
        public CadastrarProdutoCommand()
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarProdutoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}