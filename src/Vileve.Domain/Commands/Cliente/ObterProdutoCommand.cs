using MediatR;
using Vileve.Domain.Validations.Cliente;

namespace Vileve.Domain.Commands.Cliente
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