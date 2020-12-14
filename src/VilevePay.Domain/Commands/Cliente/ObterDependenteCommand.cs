using System;
using MediatR;
using VilevePay.Domain.Validations.Cliente;

namespace VilevePay.Domain.Commands.Cliente
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