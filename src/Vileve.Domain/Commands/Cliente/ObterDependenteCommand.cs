using System;
using MediatR;
using Vileve.Domain.Validations.Cliente;

namespace Vileve.Domain.Commands.Cliente
{
    public class ObterDependenteCommand : ClienteCommand, IRequest<object>
    {
        public ObterDependenteCommand(Guid clienteId)
        {
            ClienteId = clienteId;
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterDependenteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}