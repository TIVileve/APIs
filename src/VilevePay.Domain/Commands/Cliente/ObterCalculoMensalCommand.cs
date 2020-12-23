using System;
using MediatR;
using VilevePay.Domain.Validations.Cliente;

namespace VilevePay.Domain.Commands.Cliente
{
    public class ObterCalculoMensalCommand : ClienteCommand, IRequest<object>
    {
        public ObterCalculoMensalCommand(Guid clienteId)
        {
            ClienteId = clienteId;
        }

        public override bool IsValid()
        {
            ValidationResult = new ObterCalculoMensalCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}