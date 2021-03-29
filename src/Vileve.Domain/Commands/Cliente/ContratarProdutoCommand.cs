using System;
using MediatR;
using Vileve.Domain.Validations.Cliente;

namespace Vileve.Domain.Commands.Cliente
{
    public class ContratarProdutoCommand : ClienteCommand, IRequest<object>
    {
        public ContratarProdutoCommand(Guid clienteId)
        {
            ClienteId = clienteId;
        }

        public override bool IsValid()
        {
            ValidationResult = new ContratarProdutoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}