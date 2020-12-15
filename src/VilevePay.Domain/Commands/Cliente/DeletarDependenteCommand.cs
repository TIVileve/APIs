using System;
using MediatR;
using VilevePay.Domain.Validations.Cliente;

namespace VilevePay.Domain.Commands.Cliente
{
    public class DeletarDependenteCommand : ClienteCommand, IRequest<bool>
    {
        public DeletarDependenteCommand(Guid clienteId, Guid dependenteId)
        {
            ClienteId = clienteId;
            DependenteId = dependenteId;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeletarDependenteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}