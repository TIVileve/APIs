using System;
using MediatR;
using Vileve.Domain.Validations.Cliente;

namespace Vileve.Domain.Commands.Cliente
{
    public class ObterDependentePorIdCommand : ClienteCommand, IRequest<object>
    {
        public ObterDependentePorIdCommand(Guid clienteId, Guid dependenteId)
        {
            ClienteId = clienteId;
            DependenteId = dependenteId;
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterDependentePorIdCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}