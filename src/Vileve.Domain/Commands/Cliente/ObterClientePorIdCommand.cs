using System;
using MediatR;
using Vileve.Domain.Validations.Cliente;

namespace Vileve.Domain.Commands.Cliente
{
    public class ObterClientePorIdCommand : ClienteCommand, IRequest<object>
    {
        public ObterClientePorIdCommand(Guid clienteId)
        {
            ClienteId = clienteId;
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterClientePorIdCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}