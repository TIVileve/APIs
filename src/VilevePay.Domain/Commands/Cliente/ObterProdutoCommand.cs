using MediatR;
using VilevePay.Domain.Validations.Cliente;

namespace VilevePay.Domain.Commands.Cliente
{
    public class ObterProdutoCommand : ClienteCommand, IRequest<object>
    {
        public ObterProdutoCommand()
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterProdutoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}