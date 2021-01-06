using MediatR;
using Vileve.Domain.Validations.Cliente;

namespace Vileve.Domain.Commands.Cliente
{
    public class CadastrarClienteCommand : ClienteCommand, IRequest<object>
    {
        public CadastrarClienteCommand()
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarClienteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}